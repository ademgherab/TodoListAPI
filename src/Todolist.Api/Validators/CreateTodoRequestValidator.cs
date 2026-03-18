using FluentValidation;
using Todolist.Api.Dto;

namespace Todolist.Api.Validators;

public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(255)
            .WithMessage("Title must not exceed 255 characters.")
            .Must(x => x.Trim() == x)
            .WithMessage("Title must not have leading or trailing whitespace.");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithMessage("Description must not exceed 1000 characters.")
            .Must(x => x == null || x.Trim() == x)
            .WithMessage("Description must not have leading or trailing whitespace.");

        RuleFor(x => x.Priority)
            .NotEmpty()
            .WithMessage("Priority is required.")
            .Must(priority => new[] { "Low", "Medium", "High" }.Contains(priority))
            .WithMessage("Priority must be Low, Medium, or High.");
    }
}
