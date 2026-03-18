using FluentValidation;
using Todolist.Api.Dto;

namespace Todolist.Api.Validators;

public sealed class GoogleIdTokenRequestValidator : AbstractValidator<GoogleIdTokenRequest>
{
    public GoogleIdTokenRequestValidator()
    {
        RuleFor(x => x.IdToken).NotEmpty();
    }
}
