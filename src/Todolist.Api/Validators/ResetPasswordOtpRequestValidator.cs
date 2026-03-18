using FluentValidation;
using Todolist.Api.Dto;

namespace Todolist.Api.Validators;

public sealed class ResetPasswordOtpRequestValidator : AbstractValidator<ResetPasswordOtpRequest>
{
    public ResetPasswordOtpRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Otp).NotEmpty().Matches("^\\d{6}$");
        RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(8);
    }
}
