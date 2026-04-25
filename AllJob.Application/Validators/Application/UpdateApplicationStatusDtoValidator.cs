using AllJob.Application.DTOs.Application;
using FluentValidation;

namespace AllJob.Application.Validators.Application;

public class UpdateApplicationStatusDtoValidator
    : AbstractValidator<UpdateApplicationStatusDto>
{
    public UpdateApplicationStatusDtoValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid application status");
    }
}