using AllJob.Application.DTOs.Management;
using FluentValidation;

namespace AllJob.Application.Validators.Management;

public class UpdatePlanDtoValidator : AbstractValidator<UpdatePlanDto>
{
    public UpdatePlanDtoValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .When(x => x.Price is not null)
            .WithMessage("Price cannot be negative");

        RuleFor(x => x.MaxJobListings)
            .GreaterThan(0)
            .When(x => x.MaxJobListings is not null)
            .WithMessage("MaxJobListings must be greater than 0");
    }
}