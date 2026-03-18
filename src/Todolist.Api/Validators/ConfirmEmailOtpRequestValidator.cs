using FluentValidation;
using Todolist.Api.Dto;

namespace Todolist.Api.Validators;

public sealed class ConfirmEmailOtpRequestValidator : AbstractValidator<ConfirmEmailOtpRequest>
{
    public ConfirmEmailOtpRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Otp).NotEmpty().Matches("^\\d{6}$");
    }
}
