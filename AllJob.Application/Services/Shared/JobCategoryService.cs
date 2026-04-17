using AllJob.Application.DTOs.JobCategory;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Jobs;

namespace AllJob.Application.Services.Shared;

public class JobCategoryService(
    IGenericRepository<JobCategory> categoryRepository,
    ICacheService cacheService,
    IUnitOfWork unitOfWork) : IJobCategoryService
{
    private const string CacheKey = "categories:all";

    public async Task<IReadOnlyList<JobCategoryResponseDto>> GetAllAsync()
    {
        var cached = cacheService.Get<IReadOnlyList<JobCategoryResponseDto>>(CacheKey);
        if (cached is not null) return cached;

        var categories = await categoryRepository.GetAllAsync();
        var result = categories.Select(c => c.ToDto()).ToList();

        cacheService.Set(CacheKey, result, TimeSpan.FromHours(1));
        return result;
    }

    public async Task<JobCategoryResponseDto> CreateAsync(CreateJobCategoryDto dto)
    {
        var category = dto.ToEntity();
        await categoryRepository.AddAsync(category);
        await unitOfWork.SaveChangesAsync();

        cacheService.Remove(CacheKey);
        return category.ToDto();
    }
}