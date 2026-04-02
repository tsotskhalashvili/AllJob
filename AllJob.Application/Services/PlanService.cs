using AllJob.Application.DTOs.Subscription;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Services;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Subscriptions;

namespace AllJob.Application.Services;

public class PlanService(
    IGenericRepository<Plan> planRepository) : IPlanService
{
    public async Task<IReadOnlyList<PlanResponseDto>> GetAllAsync()
    {
        var plans = await planRepository.GetAllAsync();
        return plans.Select(p => p.ToDto()).ToList();
    }
}