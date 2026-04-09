namespace AllJob.Application.DTOs.Blog;

public record CreateBlogPostDto(
    string Title,
    string Slug,
    string Body,
    string? CoverImageUrl,
    Guid BlogCategoryId
);
