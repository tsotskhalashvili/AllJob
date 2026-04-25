using FluentValidation;

namespace AllJob.Application.Validators;

public static class PasswordRules
{
    public static IRuleBuilderOptions<T, string> ApplyPasswordRules<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(64)
            .Matches(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])")
            .WithMessage("Password must contain uppercase, number and special character");
    }
}