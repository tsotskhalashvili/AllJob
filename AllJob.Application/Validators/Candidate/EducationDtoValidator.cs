using AllJob.Application.DTOs.Candidate;
using AllJob.Domain.Enums.Candidate;
using FluentValidation;

namespace AllJob.Application.Validators.Candidate;

public class EducationDtoValidator : AbstractValidator<EducationDto>
{
    public EducationDtoValidator()
    {
        RuleFor(x => x.InstitutionName)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.Degree)
            .NotEmpty()
            .Must(d => Enum.TryParse<DegreeType>(d, out _))
            .WithMessage("Invalid degree type");

        RuleFor(x => x.FieldOfStudy)
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