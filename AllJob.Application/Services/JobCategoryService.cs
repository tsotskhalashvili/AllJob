using AllJob.Application.DTOs.JobCategory;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Services;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Jobs;

namespace AllJob.Application.Services;

public class JobCategoryService(
    IGenericRepository<JobCategory> categoryRepository,
    IUnitOfWork unitOfWork) : IJobCategoryService
{
    public async Task<IReadOnlyList<JobCategoryResponseDto>> GetAllAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return categories.Select(c => c.ToDto()).ToList();
    }

    public async Task<JobCategoryResponseDto> CreateAsync(CreateJobCategoryDto dto)
    {
        var category = dto.ToEntity();
        await categoryRepository.AddAsync(category);
        await unitOfWork.SaveChangesAsync();
        return category.ToDto();
    }
}