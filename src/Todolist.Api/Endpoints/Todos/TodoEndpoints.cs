using Microsoft.AspNetCore.Mvc;
using Todolist.Api.Dto;
using Todolist.Api.Endpoints.Shared;

namespace Todolist.Api.Endpoints.Todos;

public static class TodoEndpoints
{
    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/todos").RequireAuthorization();
        MapTodoRoutes(group);
        return endpoints;
    }

    public static void MapTodoRoutes(RouteGroupBuilder group)
    {
        group
            .MapPost("", TodoHandlers.CreateAsync)
            .WithFluentValidation<CreateTodoRequest>()
            .Produces<TodoResponse>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

        group
            .MapGet("", TodoHandlers.ListAsync)
            .Produces<IEnumerable<TodoResponse>>(StatusCodes.Status200OK);

        group
            .MapGet("/{id:guid}", TodoHandlers.GetByIdAsync)
            .WithName("GetTodoById")
            .Produces<TodoResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound);

        group
            .MapPut("/{id:guid}", TodoHandlers.UpdateAsync)
            .WithFluentValidation<UpdateTodoRequest>()
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

        group
            .MapPatch("/{id:guid}/complete", TodoHandlers.CompleteAsync)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound);

        group
            .MapDelete("/{id:guid}", TodoHandlers.DeleteAsync)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound);
    }
}
