using AllJob.Application.DTOs.Management;
using FluentValidation;

namespace AllJob.Application.Validators.Management;

public class UpdateAdminRoleDtoValidator : AbstractValidator<UpdateAdminRoleDto>
{
    public UpdateAdminRoleDtoValidator()
    {
        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("Invalid admin role");
    }
}