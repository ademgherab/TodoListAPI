namespace Todolist.Api.Email;

public sealed class SmtpOptions
{
    public string Host { get; init; } = string.Empty;
    public int Port { get; init; } = 587;
    public string? Username { get; init; }
    public string? Password { get; init; }

    public string FromEmail { get; init; } = string.Empty;
    public string? FromName { get; init; }

    // Note: some SMTP servers expect STARTTLS on port 587; SmtpClient uses EnableSsl for both
    // implicit SSL and STARTTLS, depending on server capabilities.
    public bool EnableSsl { get; init; } = true;

    public int TimeoutSeconds { get; init; } = 30;
}
