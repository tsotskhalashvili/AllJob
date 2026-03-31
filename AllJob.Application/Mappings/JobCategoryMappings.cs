using AllJob.Application.DTOs.JobCategory;
using AllJob.Domain.Entities.Jobs;

namespace AllJob.Application.Mappings;

public static class JobCategoryMappings
{
    public static JobCategoryResponseDto ToDto(this JobCategory category)
        => new(
            Id: category.Id,
            Name: category.Name,
            Slug: category.Slug,
            IconUrl: category.IconUrl
        );

    public static JobCategory ToEntity(this CreateJobCategoryDto dto)
        => new()
        {
            Name = dto.Name,
            Slug = dto.Slug,
            IconUrl = dto.IconUrl
        };
}