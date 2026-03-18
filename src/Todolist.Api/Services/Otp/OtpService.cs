using System.Security.Cryptography;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Todolist.Api.Database;
using Todolist.Api.Entities;

namespace Todolist.Api.Services.Otp;

public sealed class OtpService(
    ApplicationDbContext db,
    IDataProtectionProvider dataProtectionProvider
) : IOtpService
{
    private const int OtpLength = 6;
    private static readonly TimeSpan OtpTtl = TimeSpan.FromMinutes(10);
    private static readonly TimeSpan ResendCooldown = TimeSpan.FromSeconds(60);
    private const int MaxAttempts = 5;
    private const string OtpProtectorPurpose = "user-otp-token-v1";

    public async Task<string?> CreateOrThrottleAsync(
        string userId,
        string purpose,
        Func<Task<string>> tokenFactory,
        CancellationToken cancellationToken
    )
    {
        var now = DateTime.UtcNow;

        var existing = await db
            .UserOtps.Where(o =>
                o.UserId == userId
                && o.Purpose == purpose
                && o.ConsumedAt == null
                && o.ExpiresAt > now
            )
            .OrderByDescending(o => o.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (existing is not null && now - existing.CreatedAt < ResendCooldown)
        {
            return null;
        }

        if (existing is not null)
        {
            existing.ConsumedAt = now;
        }

        var otp = GenerateOtpCode();
        var salt = GenerateSalt();
        var hash = HashOtp(salt, otp);

        var token = await tokenFactory();
        var protector = dataProtectionProvider.CreateProtector(OtpProtectorPurpose);
        var tokenProtected = protector.Protect(token);

        var record = new UserOtp
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Purpose = purpose,
            CodeSalt = salt,
            CodeHash = hash,
            TokenProtected = tokenProtected,
            CreatedAt = now,
            ExpiresAt = now.Add(OtpTtl),
            AttemptCount = 0,
        };

        db.UserOtps.Add(record);
        await db.SaveChangesAsync(cancellationToken);

        return otp;
    }

    public async Task<string?> TryConsumeAsync(
        string userId,
        string purpose,
        string otp,
        CancellationToken cancellationToken
    )
    {
        var now = DateTime.UtcNow;
        var record = await db
            .UserOtps.Where(o =>
                o.UserId == userId
                && o.Purpose == purpose
                && o.ConsumedAt == null
                && o.ExpiresAt > now
            )
            .OrderByDescending(o => o.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (record is null)
        {
            return null;
        }

        if (record.AttemptCount >= MaxAttempts)
        {
            record.ConsumedAt = now;
            await db.SaveChangesAsync(cancellationToken);
            return null;
        }

        var expected = HashOtp(record.CodeSalt, otp);
        var ok = FixedTimeEqualsBase64(record.CodeHash, expected);

        if (!ok)
        {
            record.AttemptCount++;
            if (record.AttemptCount >= MaxAttempts)
            {
                record.ConsumedAt = now;
            }

            await db.SaveChangesAsync(cancellationToken);
            return null;
        }

        record.ConsumedAt = now;
        await db.SaveChangesAsync(cancellationToken);

        var protector = dataProtectionProvider.CreateProtector(OtpProtectorPurpose);
        return protector.Unprotect(record.TokenProtected);
    }

    private static string GenerateOtpCode()
    {
        // 6 digits, cryptographically secure.
        Span<byte> bytes = stackalloc byte[4];
        RandomNumberGenerator.Fill(bytes);
        var value = BitConverter.ToUInt32(bytes);
        var mod = (int)(value % 1_000_000);
        return mod.ToString($"D{OtpLength}");
    }

    private static string GenerateSalt()
    {
        Span<byte> bytes = stackalloc byte[16];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }

    private static string HashOtp(string saltBase64, string otp)
    {
        var salt = Convert.FromBase64String(saltBase64);
        var otpBytes = System.Text.Encoding.UTF8.GetBytes(otp);
        var data = new byte[salt.Length + otpBytes.Length];
        Buffer.BlockCopy(salt, 0, data, 0, salt.Length);
        Buffer.BlockCopy(otpBytes, 0, data, salt.Length, otpBytes.Length);

        var hash = SHA256.HashData(data);
        return Convert.ToBase64String(hash);
    }

    private static bool FixedTimeEqualsBase64(string aBase64, string bBase64)
    {
        try
        {
            var a = Convert.FromBase64String(aBase64);
            var b = Convert.FromBase64String(bBase64);
            return CryptographicOperations.FixedTimeEquals(a, b);
        }
        catch
        {
            return false;
        }
    }
}
