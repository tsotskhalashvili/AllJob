using AllJob.Application.DTOs.Auth;
using FluentValidation;

namespace AllJob.Application.Validators.Auth;

public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordDtoValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(64)
            .Matches(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])")
            .WithMessage("Password must contain uppercase, number and special character");

    }

}
