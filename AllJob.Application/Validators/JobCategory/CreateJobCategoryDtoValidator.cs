using AllJob.Application.DTOs.JobCategory;
using FluentValidation;

namespace AllJob.Application.Validators.JobCategory;

public class CreateJobCategoryDtoValidator
    : AbstractValidator<CreateJobCategoryDto>
{
    public CreateJobCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(100)
            .Matches(@"^[a-z0-9-]+$")
            .WithMessage("Slug must contain only lowercase letters, numbers and hyphens");

        RuleFor(x => x.IconUrl)
            .MaximumLength(512)
            .Must(u => Uri.TryCreate(u, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.IconUrl))
            .WithMessage("IconUrl must be a valid URL");
    }
}