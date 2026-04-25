using AllJob.Application.DTOs.Auth;
using FluentValidation;

namespace AllJob.Application.Validators.Auth;

public class RegisterDtoValidator : AbstractValidator<RegisterDto> 
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .MaximumLength(256);

        RuleFor(x => x.Password).ApplyPasswordRules();

        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(r => r == "Candidate" || r == "Employer")
            .WithMessage("Role must be Candidate or Employer");
    }
}
