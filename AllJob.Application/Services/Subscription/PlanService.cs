using AllJob.Application.DTOs.Subscription;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Interfaces.Services.Subscription;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Subscriptions;

namespace AllJob.Application.Services.Subscription;

public class PlanService(
    IGenericRepository<Plan> planRepository,
    ICacheService cacheService) : IPlanService
{
    private const string CacheKey = "plans:all";

    public async Task<IReadOnlyList<PlanResponseDto>> GetAllAsync()
    {
        var cached = cacheService.Get<IReadOnlyList<PlanResponseDto>>(CacheKey);
        if (cached is not null) return cached;

        var plans = await planRepository.GetAllAsync();
        var result = plans.Select(p => p.ToDto()).ToList();

        cacheService.Set(CacheKey, result, TimeSpan.FromHours(1));
        return result;
    }
}