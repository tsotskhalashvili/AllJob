using AllJob.Application.DTOs.Auth;
using FluentValidation;

namespace AllJob.Application.Validators.Auth;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
        .NotEmpty();
        
    }
}
