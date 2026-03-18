namespace Todolist.Api.Endpoints.Auth;

public static class AuthEmailTemplates
{
    public static string BuildOtpEmailHtml(string title, string otp, int expiresInMinutes)
    {
        return $"""
            <h2>{System.Net.WebUtility.HtmlEncode(title)}</h2>
            <p>Your one-time code is:</p>
            <p style="font-size: 24px; letter-spacing: 4px;"><b>{System.Net.WebUtility.HtmlEncode(
                otp
            )}</b></p>
            <p>This code expires in {expiresInMinutes} minutes.</p>
            """;
    }
}
