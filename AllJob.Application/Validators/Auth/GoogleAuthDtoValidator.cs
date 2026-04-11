using AllJob.Application.DTOs.Auth;
using FluentValidation;

namespace AllJob.Application.Validators.Auth;

public class GoogleAuthDtoValidator : AbstractValidator<GoogleAuthDto>
{
    public GoogleAuthDtoValidator()
    {
        RuleFor(x => x.IdToken)
            .NotEmpty();

        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(r => r == "Candidate" || r == "Employer")
            .WithMessage("Role must be Candidate or Employer");
    }
}
 