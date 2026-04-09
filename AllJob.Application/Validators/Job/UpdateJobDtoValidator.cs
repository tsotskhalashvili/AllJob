using AllJob.Application.DTOs.Job;
using FluentValidation;

namespace AllJob.Application.Validators.Job;

public class UpdateJobDtoValidator : AbstractValidator<UpdateJobDto>
{
    public UpdateJobDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(256)
            .When(x => x.Title is not null);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(5000)
            .When(x => x.Description is not null);

        RuleFor(x => x.SalaryMin)
            .GreaterThan(0)
            .When(x => x.SalaryMin.HasValue);

        RuleFor(x => x.SalaryMax)
            .GreaterThan(0)
            .When(x => x.SalaryMax.HasValue);

        RuleFor(x => x.SalaryMax)
            .GreaterThanOrEqualTo(x => x.SalaryMin!.Value)
            .When(x => x.SalaryMin.HasValue && x.SalaryMax.HasValue)
            .WithMessage("SalaryMax must be greater than SalaryMin");

        RuleFor(x => x.ExpiresAt)
            .Must(d => d > DateTime.UtcNow)
            .When(x => x.ExpiresAt.HasValue)
            .WithMessage("ExpiresAt must be in the future");
    }
}