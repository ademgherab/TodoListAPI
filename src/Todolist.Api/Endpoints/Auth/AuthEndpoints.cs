using Todolist.Api.Dto;
using Todolist.Api.Endpoints.Shared;

namespace Todolist.Api.Endpoints.Auth;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/auth");
        MapAuthRoutes(group);
        return endpoints;
    }

    public static void MapAuthRoutes(RouteGroupBuilder group)
    {
        group
            .MapPost("/register", AuthHandlers.RegisterAsync)
            .WithFluentValidation<RegisterRequest>()
            .AllowAnonymous();

        group
            .MapPost("/login", AuthHandlers.LoginAsync)
            .WithFluentValidation<LoginRequest>()
            .AllowAnonymous();

        group
            .MapPost("/resend-confirmation-email", AuthHandlers.ResendConfirmationEmailAsync)
            .WithFluentValidation<EmailRequest>()
            .AllowAnonymous();

        group
            .MapPost("/confirm-email", AuthHandlers.ConfirmEmailAsync)
            .WithFluentValidation<ConfirmEmailOtpRequest>()
            .AllowAnonymous();

        group
            .MapPost("/forgot-password", AuthHandlers.ForgotPasswordAsync)
            .WithFluentValidation<EmailRequest>()
            .AllowAnonymous();

        group
            .MapPost("/reset-password", AuthHandlers.ResetPasswordAsync)
            .WithFluentValidation<ResetPasswordOtpRequest>()
            .AllowAnonymous();

        group
            .MapPost("/external/google", AuthHandlers.GoogleExchangeAsync)
            .WithFluentValidation<GoogleIdTokenRequest>()
            .AllowAnonymous();
    }
}
