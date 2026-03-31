using AllJob.Application.DTOs.Company;
using FluentValidation;

namespace AllJob.Application.Validators.Company;

public class UpdateCompanyDtoValidator : AbstractValidator<UpdateCompanyDto>
{
    public UpdateCompanyDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.Website)
            .NotEmpty()
            .MaximumLength(256)
            .Must(w => Uri.TryCreate(w, UriKind.Absolute, out _))
            .WithMessage("Website must be a valid URL");

        RuleFor(x => x.Industry)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LogoUrl)
            .MaximumLength(512)
            .Must(u => Uri.TryCreate(u, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.LogoUrl))
            .WithMessage("LogoUrl must be a valid URL");
    }
}