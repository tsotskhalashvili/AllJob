using AllJob.Application.DTOs.Application;
using FluentValidation;

namespace AllJob.Application.Validators.Application;

public class CreateApplicationDtoValidator
    : AbstractValidator<CreateApplicationDto>
{
    public CreateApplicationDtoValidator()
    {
        RuleFor(x => x.JobId)
            .NotEmpty();

        RuleFor(x => x.CvUrl)
            .NotEmpty()
            .MaximumLength(512)
            .Must(u => Uri.TryCreate(u, UriKind.Absolute, out _))
            .WithMessage("CvUrl must be a valid URL");

        RuleFor(x => x.CoverLetter)
            .MaximumLength(3000)
            .When(x => !string.IsNullOrEmpty(x.CoverLetter));
    }
}