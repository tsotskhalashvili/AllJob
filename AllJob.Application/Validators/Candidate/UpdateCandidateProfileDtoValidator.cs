using AllJob.Application.DTOs.Candidate;
using FluentValidation;

namespace AllJob.Application.Validators.Candidate;

public class UpdateCandidateProfileDtoValidator
    : AbstractValidator<UpdateCandidateProfileDto>
{
    public UpdateCandidateProfileDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(100)
            .When(x => x.FirstName is not null);

        RuleFor(x => x.LastName)
            .MaximumLength(100)
            .When(x => x.LastName is not null);

        RuleFor(x => x.Bio)
            .MaximumLength(1000)
            .When(x => x.Bio is not null);

        RuleFor(x => x.LinkedInUrl)
            .MaximumLength(256)
            .Must(u => Uri.TryCreate(u, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.LinkedInUrl))
            .WithMessage("LinkedInUrl must be a valid URL");

        RuleFor(x => x.PhotoUrl)
            .MaximumLength(512)
            .Must(u => Uri.TryCreate(u, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.PhotoUrl))
            .WithMessage("PhotoUrl must be a valid URL");
    }
}