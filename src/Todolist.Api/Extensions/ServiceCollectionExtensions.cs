using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Todolist.Api.Database;
using Todolist.Api.Email;
using Todolist.Api.Entities;
using Todolist.Api.Middlewares;
using Todolist.Api.Services.Otp;
using Todolist.Api.Swagger;
using Todolist.Api.Validators;

namespace Todolist.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowAll",
                policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                }
            );
        });

        return services;
    }

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateTodoRequestValidator>();
        return services;
    }

    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("Application"))
        );

        return services;
    }

    public static IServiceCollection AddIdentityAuth(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();

        services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddEmailSender(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.Configure<SmtpOptions>(config.GetSection("Email:Smtp"));
        services.AddTransient<SmtpEmailSender>();
        services.AddTransient<IEmailSender<ApplicationUser>>(sp =>
            sp.GetRequiredService<SmtpEmailSender>()
        );
        return services;
    }

    public static IServiceCollection AddHealth(this IServiceCollection services)
    {
        services.AddHealthChecks();
        return services;
    }

    public static IServiceCollection AddSwaggerWithAuth(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1",
                new()
                {
                    Title = "Todo List API",
                    Version = "v1",
                    Description = "Manage your todo tasks",
                }
            );

            options.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "Bearer",
                    In = ParameterLocation.Header,
                    Description = "Paste a bearer token like: Bearer {token}",
                }
            );

            options.DocumentFilter<BearerAuthDocumentFilter>();
        });

        return services;
    }

    public static IServiceCollection AddProblemDetailsAndExceptions(
        this IServiceCollection services
    )
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions.Add(
                    "requestId",
                    context.HttpContext.TraceIdentifier
                );
            };
        });

        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddScoped<IOtpService, OtpService>();

        return services
            .AddCorsPolicy()
            .AddValidation()
            .AddDatabase(config)
            .AddIdentityAuth(config)
            .AddEmailSender(config)
            .AddHealth()
            .AddSwaggerWithAuth()
            .AddProblemDetailsAndExceptions();
    }
}
