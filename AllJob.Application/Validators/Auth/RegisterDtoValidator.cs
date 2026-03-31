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

        RuleFor(x => x.Password)
          .NotEmpty()
          .MinimumLength(8)
          .MaximumLength(64)
          .Matches(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])")
          .WithMessage("Password must contain uppercase, number and special character");

        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(r => r == "Candidate" || r == "Employer")
            .WithMessage("Role must be Candidate or Employer");
    }
}
