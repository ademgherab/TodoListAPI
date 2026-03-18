namespace Todolist.Api.Services.Otp;

public interface IOtpService
{
    Task<string?> CreateOrThrottleAsync(
        string userId,
        string purpose,
        Func<Task<string>> tokenFactory,
        CancellationToken cancellationToken
    );

    Task<string?> TryConsumeAsync(
        string userId,
        string purpose,
        string otp,
        CancellationToken cancellationToken
    );
}
