using Todolist.Api.Endpoints.Auth;
using Todolist.Api.Endpoints.Health;
using Todolist.Api.Endpoints.Todos;

namespace Todolist.Api.Endpoints;

public static class ApiEndpoints
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapHealthEndpoints().MapAuthEndpoints().MapTodoEndpoints();
    }
}

