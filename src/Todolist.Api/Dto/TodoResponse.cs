namespace Todolist.Api.Dto;

public record TodoResponse(
    Guid Id,
    string Title,
    string? Description,
    string Priority,
    string Completed
);
