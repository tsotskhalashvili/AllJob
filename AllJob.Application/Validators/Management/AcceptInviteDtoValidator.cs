using AllJob.Application.DTOs.Management;
using FluentValidation;

namespace AllJob.Application.Validators.Management;

public class AcceptInviteDtoValidator : AbstractValidator<AcceptInviteDto>
{
    public AcceptInviteDtoValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100)
            .Matches("^[a-zA-Z]+$")
            .WithMessage("FirstName must be in Latin characters");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100)
            .Matches("^[a-zA-Z]+$")
            .WithMessage("LastName must be in Latin characters");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]").WithMessage("Password must contain uppercase letter")
            .Matches("[0-9]").WithMessage("Password must contain number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain special character");
    }
}