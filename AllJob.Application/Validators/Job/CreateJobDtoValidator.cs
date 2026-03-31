using AllJob.Application.DTOs.Job;
using FluentValidation;

namespace AllJob.Application.Validators.Job;

public class CreateJobDtoValidator : AbstractValidator<CreateJobDto>
{
    public CreateJobDtoValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.AddressId)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(5000);

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
            .GreaterThan(DateTime.Now)
            .WithMessage("ExpiresAt must be in the future");

        RuleFor(x => x.SkillIds)
            .NotEmpty()
            .WithMessage("At least one skill is required");
    }
}