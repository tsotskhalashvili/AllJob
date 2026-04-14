using AllJob.Application.DTOs.Blog;
using FluentValidation;

namespace AllJob.Application.Validators.Blog;

public class CreateBlogPostDtoValidator : AbstractValidator<CreateBlogPostDto>
{
    public CreateBlogPostDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(200)
            .Matches("^[a-z0-9-]+$")
            .WithMessage("Slug must contain only lowercase letters, numbers and hyphens");

        RuleFor(x => x.Body)
            .NotEmpty();

        RuleFor(x => x.BlogCategoryId)
            .NotEmpty();
    }
}