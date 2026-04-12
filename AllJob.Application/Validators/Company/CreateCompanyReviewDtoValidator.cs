using AllJob.Application.DTOs.Company;
using FluentValidation;

namespace AllJob.Application.Validators.Company;

public class CreateCompanyReviewDtoValidator
    : AbstractValidator<CreateCompanyReviewDto>
{
    public CreateCompanyReviewDtoValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5");

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.Body)
            .NotEmpty()
            .MaximumLength(2000);
    }
}