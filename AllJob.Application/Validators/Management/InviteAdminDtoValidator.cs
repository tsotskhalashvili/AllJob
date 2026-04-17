using AllJob.Application.DTOs.Management;
using FluentValidation;

namespace AllJob.Application.Validators.Management;

public class InviteAdminDtoValidator : AbstractValidator<InviteAdminDto>
{
    public InviteAdminDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("Invalid admin role");
    }
}