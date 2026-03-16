using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Todolist.Api.Middlewares;

public class ValidationExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        var context =
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails =
                {
                    Detail = "one or more validation errors occurred.",
                    Status = StatusCodes.Status400BadRequest,
                }
            };
        var errors = validationException.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                x => x.Key,
                x => x.Select(m => m.ErrorMessage).ToList()
            );
        context.ProblemDetails.Extensions.TryAdd("errors", errors);
        await problemDetailsService.TryWriteAsync(context);

        return true;
    }
}