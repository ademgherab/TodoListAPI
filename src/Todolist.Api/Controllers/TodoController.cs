using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolist.Api.Database;
using Todolist.Api.Dto;
using Todolist.Api.Enums;
using Todolist.Api.Entities;
using System.Net.Mime;
using FluentValidation;

namespace Todolist.Api.Controllers;

[ApiController]
[Route("/api/todos")]
[Produces(MediaTypeNames.Application.Json)]
public class TodosController(ApplicationDbContext dbContext) : Controller
{
    [HttpPost]
    [ProducesResponseType(typeof(TodoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTodo(
        [FromBody] CreateTodoRequest request,
        IValidator<CreateTodoRequest> validator,
        CancellationToken cancellationToken = default)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var existingTodo = await dbContext.TodoItems.FirstOrDefaultAsync(t => t.Title == request.Title, cancellationToken);
        if (existingTodo is not null)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Todo Conflict",
                Detail = "A todo with the same title already exists.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var newTodo = new Todo
        {
            Title = request.Title,
            Description = request.Description,
            Priority = Enum.Parse<TodoPriority>(request.Priority, true),
            CreatedAt = DateTime.UtcNow
        };
        
        await dbContext.TodoItems.AddAsync(newTodo, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetTodo), new { id = newTodo.Id }, MapToTodoResponse(newTodo));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TodoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTodo(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var todo = await dbContext
            .TodoItems
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (todo is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Todo Not Found",
                Detail = "The requested todo does not exist.",
                Status = StatusCodes.Status404NotFound
            });
        }
        var todoResponse = MapToTodoResponse(todo);
        return Ok(todoResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TodoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTodos(CancellationToken cancellationToken = default)
    {
        var todos = await dbContext.TodoItems
            .Select(t => MapToTodoResponse(t))
            .ToListAsync(cancellationToken);
        
        return Ok(todos);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTodo(
        [FromRoute] Guid id,
        [FromBody] UpdateTodoRequest request,
        IValidator<UpdateTodoRequest> validator,
        CancellationToken cancellationToken = default)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var todo = await dbContext.TodoItems.FindAsync(id, cancellationToken);
        if (todo is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Todo Not Found",
                Detail = "The requested todo does not exist.",
                Status = StatusCodes.Status404NotFound
            });
        }

        var existingTodo = await dbContext.TodoItems.FirstOrDefaultAsync(t => t.Title == request.Title, cancellationToken);
        if (existingTodo is not null)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Todo Conflict",
                Detail = "A todo with the same title already exists.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        todo.Title = request.Title;
        todo.Description = request.Description;
        todo.Priority = Enum.Parse<TodoPriority>(request.Priority, true);

        await dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id}/complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkTodoAsCompleted(Guid id, CancellationToken cancellationToken = default)
    {
        var todo = await dbContext.TodoItems.FindAsync(id, cancellationToken);
        if (todo is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Todo Not Found",
                Detail = "The requested todo does not exist.",
                Status = StatusCodes.Status404NotFound
            });
        }

        todo.CompletedAt = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTodo(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var todo = await dbContext.TodoItems.FindAsync(id, cancellationToken);
        if (todo is null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Todo Not Found",
                Detail = "The requested todo does not exist.",
                Status = StatusCodes.Status404NotFound
            });
        }

        dbContext.TodoItems.Remove(todo);
        await dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
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
}