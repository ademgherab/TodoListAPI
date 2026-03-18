using FluentValidation;
using Todolist.Api.Dto;

namespace Todolist.Api.Validators;

public sealed class EmailRequestValidator : AbstractValidator<EmailRequest>
{
    public EmailRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
