namespace Todolist.Api.Entities;

public sealed class UserOtp
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = default!;

    // e.g. "confirm_email" or "reset_password"
    public string Purpose { get; set; } = string.Empty;

    public string CodeSalt { get; set; } = string.Empty;
    public string CodeHash { get; set; } = string.Empty;

    // Data-protected Identity token (email-confirm or reset-password).
    public string TokenProtected { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ConsumedAt { get; set; }

    public int AttemptCount { get; set; }
}
