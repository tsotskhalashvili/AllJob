using AllJob.Application.DTOs.Candidate;
using FluentValidation;

namespace AllJob.Application.Validators.Candidate;

public class CreateCandidateProfileDtoValidator
    : AbstractValidator<CreateCandidateProfileDto>
{
    public CreateCandidateProfileDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Bio)
            .MaximumLength(1000);

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

        RuleFor(x => x.AddressId)
            .NotEmpty();

        RuleFor(x => x.SkillIds)
            .NotEmpty()
            .WithMessage("At least one skill is required");
    }
}