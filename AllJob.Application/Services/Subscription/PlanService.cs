using AllJob.Application.DTOs.Subscription;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Services.Subscription;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Subscriptions;

namespace AllJob.Application.Services.Subscription;

public class PlanService(
    IGenericRepository<Plan> planRepository) : IPlanService
{
    public async Task<IReadOnlyList<PlanResponseDto>> GetAllAsync()
    {
        var plans = await planRepository.GetAllAsync();
        return plans.Select(p => p.ToDto()).ToList();
    }
}