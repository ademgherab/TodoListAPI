using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Todolist.Api.Entities;

namespace Todolist.Api.Email;

public sealed class SmtpEmailSender(IOptions<SmtpOptions> options) : IEmailSender<ApplicationUser>
{
    private readonly SmtpOptions _options = options.Value;

    public Task SendConfirmationLinkAsync(
        ApplicationUser user,
        string email,
        string confirmationLink
    ) =>
        SendEmailCoreAsync(
            email,
            "Confirm your email",
            BuildLinkBody("Confirm your email", confirmationLink)
        );

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
        SendEmailCoreAsync(
            email,
            "Reset your password",
            BuildLinkBody("Reset your password", resetLink)
        );

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
        SendEmailCoreAsync(
            email,
            "Reset your password",
            BuildCodeBody("Reset your password", resetCode)
        );

    // Some Identity flows may still call a basic "send email" method depending on how endpoints are used.
    public Task SendEmailAsync(string email, string subject, string htmlMessage) =>
        SendEmailCoreAsync(email, subject, htmlMessage);

    private async Task SendEmailCoreAsync(string toEmail, string subject, string htmlBody)
    {
        if (string.IsNullOrWhiteSpace(_options.Host))
        {
            throw new InvalidOperationException(
                "SMTP host is not configured. Set Email:Smtp:Host."
            );
        }

        if (string.IsNullOrWhiteSpace(_options.FromEmail))
        {
            throw new InvalidOperationException(
                "SMTP FromEmail is not configured. Set Email:Smtp:FromEmail."
            );
        }

        using var message = new MailMessage
        {
            From = new MailAddress(_options.FromEmail, _options.FromName),
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true,
        };
        message.To.Add(new MailAddress(toEmail));

        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            EnableSsl = _options.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Timeout = Math.Max(1, _options.TimeoutSeconds) * 1000,
        };

        if (!string.IsNullOrWhiteSpace(_options.Username))
        {
            client.Credentials = new NetworkCredential(_options.Username, _options.Password);
        }

        // SmtpClient has no true async API on all TFMs; SendMailAsync exists and is fine for our usage.
        await client.SendMailAsync(message);
    }

    private static string BuildLinkBody(string title, string link)
    {
        var normalizedLink = NormalizeUrl(link);
        var safeTitle = HtmlEncoder.Default.Encode(title);
        var safeLink = HtmlEncoder.Default.Encode(normalizedLink);

        return $"""
            <h2>{safeTitle}</h2>
            <p><a href="{safeLink}">Click here</a></p>
            <p>If the link does not work, copy and paste this URL:</p>
            <p><code>{safeLink}</code></p>
            """;
    }

    private static string NormalizeUrl(string url)
    {
        // Identity may pass a link that is already HTML-encoded (e.g. contains &amp;).
        // Decode first, then rebuild the URL so query values are properly percent-encoded.
        var decoded = WebUtility.HtmlDecode(url).Trim();

        if (!Uri.TryCreate(decoded, UriKind.Absolute, out var uri))
        {
            // Best-effort fallback: at least return the decoded string.
            return decoded;
        }

        var baseUri = uri.GetLeftPart(UriPartial.Path);
        var query = QueryHelpers.ParseQuery(uri.Query);

        // Flatten to key/value pairs. QueryHelpers.AddQueryString will percent-encode values.
        var pairs = query.SelectMany(
            kvp => kvp.Value,
            (kvp, v) => new KeyValuePair<string, string?>(kvp.Key, v)
        );

        var rebuilt = QueryHelpers.AddQueryString(baseUri, pairs);
        if (!string.IsNullOrEmpty(uri.Fragment))
        {
            rebuilt += uri.Fragment;
        }

        return rebuilt;
    }

    private static string BuildCodeBody(string title, string code) =>
        $"""
            <h2>{WebUtility.HtmlEncode(title)}</h2>
            <p>Your code is:</p>
            <p><b>{WebUtility.HtmlEncode(code)}</b></p>
            """;
}
