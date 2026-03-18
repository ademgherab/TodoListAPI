namespace Todolist.Api.Endpoints.Shared;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithFluentValidation<TRequest>(
        this RouteHandlerBuilder builder
    )
    {
        return builder.AddEndpointFilter(new FluentValidationEndpointFilter<TRequest>());
    }
}
