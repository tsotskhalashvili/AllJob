namespace AllJob.Application.DTOs.JobCategory;

public record CreateJobCategoryDto(
    string Name,
    string Slug,
    string IconUrl
);
