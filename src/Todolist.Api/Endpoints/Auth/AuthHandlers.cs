using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todolist.Api.Dto;
using Todolist.Api.Email;
using Todolist.Api.Entities;
using Todolist.Api.Services.Otp;

namespace Todolist.Api.Endpoints.Auth;

public static class AuthHandlers
{
    private const string PurposeConfirmEmail = "confirm_email";
    private const string PurposeResetPassword = "reset_password";
    private const int OtpExpiresMinutes = 10;

    public static async Task<IResult> RegisterAsync(
        [FromBody] RegisterRequest request,
        UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken
    )
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = false,
        };

        var create = await userManager.CreateAsync(user, request.Password);
        if (!create.Succeeded)
        {
            return Results.BadRequest(
                new ProblemDetails
                {
                    Title = "Registration failed",
                    Detail = string.Join("; ", create.Errors.Select(e => e.Description)),
                    Status = StatusCodes.Status400BadRequest,
                }
            );
        }

        return Results.Created("/api/auth/register", new { message = "Registered" });
    }

    public static async Task<IResult> LoginAsync(
        [FromBody] LoginRequest request,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        CancellationToken cancellationToken
    )
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Results.Problem(
                title: "Invalid credentials",
                statusCode: StatusCodes.Status401Unauthorized
            );
        }

        var check = await signInManager.CheckPasswordSignInAsync(
            user,
            request.Password,
            lockoutOnFailure: true
        );

        if (!check.Succeeded)
        {
            return Results.Problem(
                title: "Invalid credentials",
                statusCode: StatusCodes.Status401Unauthorized
            );
        }

        if (!await userManager.IsEmailConfirmedAsync(user))
        {
            return Results.Problem(
                title: "Email not confirmed",
                detail: "Confirm your email then login. Use /api/auth/resend-confirmation-email.",
                statusCode: StatusCodes.Status403Forbidden
            );
        }

        var principal = await signInManager.CreateUserPrincipalAsync(user);
        return Results.SignIn(principal, authenticationScheme: IdentityConstants.BearerScheme);
    }

    public static async Task<IResult> ResendConfirmationEmailAsync(
        [FromBody] EmailRequest request,
        UserManager<ApplicationUser> userManager,
        IOtpService otpService,
        SmtpEmailSender emailSender,
        CancellationToken cancellationToken
    )
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null || user.EmailConfirmed)
        {
            return Results.Ok();
        }

        var otp = await otpService.CreateOrThrottleAsync(
            user.Id,
            PurposeConfirmEmail,
            tokenFactory: async () => await userManager.GenerateEmailConfirmationTokenAsync(user),
            cancellationToken
        );

        if (otp is not null)
        {
            await emailSender.SendEmailAsync(
                user.Email!,
                "Email confirmation code",
                AuthEmailTemplates.BuildOtpEmailHtml(
                    "Confirm your email",
                    otp,
                    expiresInMinutes: OtpExpiresMinutes
                )
            );
        }

        return Results.Ok();
    }

    public static async Task<IResult> ConfirmEmailAsync(
        [FromBody] ConfirmEmailOtpRequest request,
        UserManager<ApplicationUser> userManager,
        IOtpService otpService,
        CancellationToken cancellationToken
    )
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return InvalidOrExpiredOtp();
        }

        var token = await otpService.TryConsumeAsync(
            user.Id,
            PurposeConfirmEmail,
            request.Otp,
            cancellationToken
        );

        if (token is null)
        {
            return InvalidOrExpiredOtp();
        }

        var result = await userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            return Results.BadRequest(
                new ProblemDetails
                {
                    Title = "Email confirmation failed",
                    Detail = string.Join("; ", result.Errors.Select(e => e.Description)),
                    Status = StatusCodes.Status400BadRequest,
                }
            );
        }

        return Results.Ok();
    }

    public static async Task<IResult> ForgotPasswordAsync(
        [FromBody] EmailRequest request,
        UserManager<ApplicationUser> userManager,
        IOtpService otpService,
        SmtpEmailSender emailSender,
        CancellationToken cancellationToken
    )
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Results.Ok();
        }

        var otp = await otpService.CreateOrThrottleAsync(
            user.Id,
            PurposeResetPassword,
            tokenFactory: async () => await userManager.GeneratePasswordResetTokenAsync(user),
            cancellationToken
        );

        if (otp is not null)
        {
            await emailSender.SendEmailAsync(
                user.Email!,
                "Password reset code",
                AuthEmailTemplates.BuildOtpEmailHtml(
                    "Reset your password",
                    otp,
                    expiresInMinutes: OtpExpiresMinutes
                )
            );
        }

        return Results.Ok();
    }

    public static async Task<IResult> ResetPasswordAsync(
        [FromBody] ResetPasswordOtpRequest request,
        UserManager<ApplicationUser> userManager,
        IOtpService otpService,
        CancellationToken cancellationToken
    )
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return InvalidOrExpiredOtp();
        }

        var token = await otpService.TryConsumeAsync(
            user.Id,
            PurposeResetPassword,
            request.Otp,
            cancellationToken
        );

        if (token is null)
        {
            return InvalidOrExpiredOtp();
        }

        var result = await userManager.ResetPasswordAsync(user, token, request.NewPassword);
        if (!result.Succeeded)
        {
            return Results.BadRequest(
                new ProblemDetails
                {
                    Title = "Reset password failed",
                    Detail = string.Join("; ", result.Errors.Select(e => e.Description)),
                    Status = StatusCodes.Status400BadRequest,
                }
            );
        }

        return Results.Ok();
    }

    public static async Task<IResult> GoogleExchangeAsync(
        [FromBody] GoogleIdTokenRequest request,
        IConfiguration configuration,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        CancellationToken cancellationToken
    )
    {
        var googleClientId = configuration["Authentication:Google:ClientId"];
        if (string.IsNullOrWhiteSpace(googleClientId))
        {
            return Results.Problem(
                title: "Google OAuth is not configured",
                detail: "Set Authentication:Google:ClientId.",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }

        GoogleJsonWebSignature.Payload payload;
        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(
                request.IdToken,
                new GoogleJsonWebSignature.ValidationSettings { Audience = [googleClientId] }
            );
        }
        catch (Exception ex)
        {
            return Results.Problem(
                title: "Invalid Google token",
                detail: ex.Message,
                statusCode: StatusCodes.Status401Unauthorized
            );
        }

        const string provider = "Google";
        var providerKey = payload.Subject;
        if (string.IsNullOrWhiteSpace(providerKey))
        {
            return Results.Problem(
                title: "Invalid Google token",
                detail: "Missing subject (sub) claim.",
                statusCode: StatusCodes.Status401Unauthorized
            );
        }

        if (!payload.EmailVerified)
        {
            return Results.Problem(
                title: "Google email not verified",
                detail: "This app requires a verified email from Google.",
                statusCode: StatusCodes.Status401Unauthorized
            );
        }

        var existingByLogin = await userManager.FindByLoginAsync(provider, providerKey);
        ApplicationUser user;

        if (existingByLogin is not null)
        {
            user = existingByLogin;
        }
        else
        {
            var email = payload.Email;
            if (string.IsNullOrWhiteSpace(email))
            {
                return Results.BadRequest(
                    new ProblemDetails
                    {
                        Title = "Google token missing email",
                        Detail =
                            "This app requires an email claim from Google to create/link an account.",
                        Status = StatusCodes.Status400BadRequest,
                    }
                );
            }

            var existingByEmail = await userManager.FindByEmailAsync(email);
            if (existingByEmail is null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                };

                var createResult = await userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    return Results.BadRequest(
                        new ProblemDetails
                        {
                            Title = "User creation failed",
                            Detail = string.Join(
                                "; ",
                                createResult.Errors.Select(e => e.Description)
                            ),
                            Status = StatusCodes.Status400BadRequest,
                        }
                    );
                }
            }
            else
            {
                user = existingByEmail;
            }

            var addLoginResult = await userManager.AddLoginAsync(
                user,
                new UserLoginInfo(provider, providerKey, provider)
            );

            if (!addLoginResult.Succeeded)
            {
                return Results.BadRequest(
                    new ProblemDetails
                    {
                        Title = "Linking Google login failed",
                        Detail = string.Join(
                            "; ",
                            addLoginResult.Errors.Select(e => e.Description)
                        ),
                        Status = StatusCodes.Status400BadRequest,
                    }
                );
            }
        }

        var principal = await signInManager.CreateUserPrincipalAsync(user);
        return Results.SignIn(principal, authenticationScheme: IdentityConstants.BearerScheme);
    }

    private static IResult InvalidOrExpiredOtp()
    {
        return Results.BadRequest(
            new ProblemDetails
            {
                Title = "Invalid or expired OTP",
                Status = StatusCodes.Status400BadRequest,
            }
        );
    }
}
