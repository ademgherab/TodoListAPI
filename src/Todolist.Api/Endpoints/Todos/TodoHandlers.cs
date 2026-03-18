using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolist.Api.Database;
using Todolist.Api.Dto;
using Todolist.Api.Entities;
using Todolist.Api.Enums;

namespace Todolist.Api.Endpoints.Todos;

public static class TodoHandlers
{
    public static async Task<IResult> CreateAsync(
        [FromBody] CreateTodoRequest request,
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        ClaimsPrincipal user,
        CancellationToken cancellationToken
    )
    {
        var ownerId = GetCurrentUserId(user, userManager);

        var existingTodo = await dbContext.TodoItems.FirstOrDefaultAsync(
            t => t.OwnerId == ownerId && t.Title == request.Title,
            cancellationToken
        );
        if (existingTodo is not null)
        {
            return TodoConflict();
        }

        var newTodo = new Todo
        {
            OwnerId = ownerId,
            Title = request.Title,
            Description = request.Description,
            Priority = Enum.Parse<TodoPriority>(request.Priority, true),
            CreatedAt = DateTime.UtcNow,
        };

        await dbContext.TodoItems.AddAsync(newTodo, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Results.CreatedAtRoute(
            routeName: "GetTodoById",
            routeValues: new { id = newTodo.Id },
            value: MapToTodoResponse(newTodo)
        );
    }

    public static async Task<IResult> GetByIdAsync(
        [FromRoute] Guid id,
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        ClaimsPrincipal user,
        CancellationToken cancellationToken
    )
    {
        var ownerId = GetCurrentUserId(user, userManager);

        var todo = await dbContext.TodoItems.FirstOrDefaultAsync(
            t => t.Id == id && t.OwnerId == ownerId,
            cancellationToken
        );

        if (todo is null)
        {
            return TodoNotFound();
        }

        return Results.Ok(MapToTodoResponse(todo));
    }

    public static async Task<IResult> ListAsync(
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        ClaimsPrincipal user,
        CancellationToken cancellationToken
    )
    {
        var ownerId = GetCurrentUserId(user, userManager);

        var items = await dbContext
            .TodoItems.Where(t => t.OwnerId == ownerId)
            .Select(t => MapToTodoResponse(t))
            .ToListAsync(cancellationToken);

        return Results.Ok(items);
    }

    public static async Task<IResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateTodoRequest request,
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        ClaimsPrincipal user,
        CancellationToken cancellationToken
    )
    {
        var ownerId = GetCurrentUserId(user, userManager);

        var todo = await dbContext.TodoItems.FirstOrDefaultAsync(
            t => t.Id == id && t.OwnerId == ownerId,
            cancellationToken
        );
        if (todo is null)
        {
            return TodoNotFound();
        }

        var titleConflict = await dbContext.TodoItems.AnyAsync(
            t => t.OwnerId == ownerId && t.Title == request.Title && t.Id != id,
            cancellationToken
        );
        if (titleConflict)
        {
            return TodoConflict();
        }

        todo.Title = request.Title;
        todo.Description = request.Description;
        todo.Priority = Enum.Parse<TodoPriority>(request.Priority, true);

        await dbContext.SaveChangesAsync(cancellationToken);
        return Results.NoContent();
    }

    public static async Task<IResult> CompleteAsync(
        [FromRoute] Guid id,
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        ClaimsPrincipal user,
        CancellationToken cancellationToken
    )
    {
        var ownerId = GetCurrentUserId(user, userManager);

        var todo = await dbContext.TodoItems.FirstOrDefaultAsync(
            t => t.Id == id && t.OwnerId == ownerId,
            cancellationToken
        );
        if (todo is null)
        {
            return TodoNotFound();
        }

        todo.CompletedAt = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
        return Results.NoContent();
    }

    public static async Task<IResult> DeleteAsync(
        [FromRoute] Guid id,
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        ClaimsPrincipal user,
        CancellationToken cancellationToken
    )
    {
        var ownerId = GetCurrentUserId(user, userManager);

        var todo = await dbContext.TodoItems.FirstOrDefaultAsync(
            t => t.Id == id && t.OwnerId == ownerId,
            cancellationToken
        );
        if (todo is null)
        {
            return TodoNotFound();
        }

        dbContext.TodoItems.Remove(todo);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Results.NoContent();
    }

    private static TodoResponse MapToTodoResponse(Todo todo)
    {
        return new TodoResponse(
            todo.Id,
            todo.Title,
            todo.Description,
            todo.Priority.ToString(),
            todo.CompletedAt.HasValue ? "Yes" : "No"
        );
    }

    private static string GetCurrentUserId(
        ClaimsPrincipal user,
        UserManager<ApplicationUser> userManager
    )
    {
        var userId = userManager.GetUserId(user);
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new InvalidOperationException(
                "Authenticated user id was not found on the current principal."
            );
        }

        return userId;
    }

    private static IResult TodoNotFound()
    {
        return Results.NotFound(
            new ProblemDetails
            {
                Title = "Todo Not Found",
                Detail = "The requested todo does not exist.",
                Status = StatusCodes.Status404NotFound,
            }
        );
    }

    private static IResult TodoConflict()
    {
        return Results.BadRequest(
            new ProblemDetails
            {
                Title = "Todo Conflict",
                Detail = "A todo with the same title already exists.",
                Status = StatusCodes.Status400BadRequest,
            }
        );
    }
}
