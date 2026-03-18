namespace Todolist.Api.Dto;

public sealed record ResetPasswordOtpRequest(string Email, string Otp, string NewPassword);

