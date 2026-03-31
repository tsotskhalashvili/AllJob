using AllJob.Application.DTOs.Skill;
using FluentValidation;

namespace AllJob.Application.Validators.Skill;

public class CreateSkillDtoValidator : AbstractValidator<CreateSkillDto>
{
    public CreateSkillDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}