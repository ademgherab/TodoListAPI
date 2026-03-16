namespace Todolist.Api.Dto;

public record UpdateTodoRequest(
    string Title,
    string? Description,
    string Priority
);