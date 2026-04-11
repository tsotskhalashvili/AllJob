using AllJob.Domain.Entities.Subscriptions;
using AllJob.Domain.Enums.Subscriptions;

namespace AllJob.Application.Interfaces.Repositories.Subscriptions;

public interface IPlanRepository : IGenericRepository<Plan>
{
    Task<Plan?> GetByTierAsync(PlanTier tier);
}