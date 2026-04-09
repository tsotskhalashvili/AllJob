    using AllJob.Application.DTOs.Company;
    using FluentValidation;

    namespace AllJob.Application.Validators.Company;

    public class UpdateCompanyDtoValidator : AbstractValidator<UpdateCompanyDto>
    {
        public UpdateCompanyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(256)
                .When(x => x.Name is not null);

            RuleFor(x => x.Website)
          
                .MaximumLength(256)
                .Must(w => Uri.TryCreate(w, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.Website))
                .WithMessage("Website must be a valid URL");

            RuleFor(x => x.Industry)
                .NotEmpty()
                .MaximumLength(100)
                .When(x => x.Industry is not null);

            RuleFor(x => x.LogoUrl)
                .MaximumLength(512)
                .Must(u => Uri.TryCreate(u, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.LogoUrl))
                .WithMessage("LogoUrl must be a valid URL");
        }
    }