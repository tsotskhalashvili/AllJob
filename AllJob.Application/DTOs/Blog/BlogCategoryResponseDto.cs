namespace AllJob.Application.DTOs.Blog;

public record BlogCategoryResponseDto(
    Guid Id,
    string Name,
    string Slug
    );