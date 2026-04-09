namespace AllJob.Application.DTOs.Blog;

public record BlogPostResponseDto(
    Guid Id,
    string Title,
    string Slug,
    string Body,
    string? CoverImageUrl,
    string AuthorName,
    string CategoryName,
    bool IsPublished,
    int ViewCount,
    DateTime CreatedAt
    );

