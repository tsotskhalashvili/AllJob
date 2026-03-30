namespace AllJob.Application.DTOs.JobCategory;

public record JobCategoryResponseDto(
    Guid Id,
    string Name,
    string Slug,
    string IconUrl
);
