using FluentValidation;
using FluentValidation.Results;

namespace Todolist.Api.Endpoints.Shared;

public sealed class FluentValidationEndpointFilter<TRequest> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next
    )
    {
        TRequest? request = default;
        for (var i = 0; i < context.Arguments.Count; i++)
        {
            if (context.Arguments[i] is TRequest typed)
            {
                request = typed;
                break;
            }
        }

        if (request is null)
        {
            throw new ValidationException(
                "Request body is required.",
                new[] { new ValidationFailure("body", "Request body is required.") }
            );
        }

        var validator = context.HttpContext.RequestServices.GetRequiredService<
            IValidator<TRequest>
        >();
        await validator.ValidateAndThrowAsync(request, context.HttpContext.RequestAborted);

        return await next(context);
    }
}
