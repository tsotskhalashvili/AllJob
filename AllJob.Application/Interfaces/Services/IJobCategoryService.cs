using AllJob.Application.DTOs.JobCategory;

namespace AllJob.Application.Interfaces.Services;

public interface IJobCategoryService
{
    Task<IReadOnlyList<JobCategoryResponseDto>> GetAllAsync();
    Task<JobCategoryResponseDto> CreateAsync(CreateJobCategoryDto dto);
}