using AllJob.Application.DTOs.Auth;
using FluentValidation;

namespace AllJob.Application.Validators.Auth;

public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordDtoValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty();

        RuleFor(x => x.NewPassword).ApplyPasswordRules();
    }

}
