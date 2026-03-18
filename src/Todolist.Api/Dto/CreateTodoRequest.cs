namespace Todolist.Api.Dto;

public record CreateTodoRequest(string Title, string? Description, string Priority);
