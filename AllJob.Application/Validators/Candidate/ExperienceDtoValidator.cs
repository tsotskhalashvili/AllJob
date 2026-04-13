using AllJob.Application.DTOs.Candidate;
using FluentValidation;

namespace AllJob.Application.Validators.Candidate;

public class ExperienceDtoValidator : AbstractValidator<ExperienceDto>
{
    public ExperienceDtoValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.Position)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("StartDate cannot be in the future");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .When(x => x.EndDate.HasValue)
            .WithMessage("EndDate must be after StartDate");
    }
}