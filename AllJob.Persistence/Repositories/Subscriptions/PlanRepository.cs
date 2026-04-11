using AllJob.Application.Interfaces.Repositories.Subscriptions;
using AllJob.Domain.Entities.Subscriptions;
using AllJob.Domain.Enums.Subscriptions;
using AllJob.Persistence.Context;
using AllJob.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Subscriptions;

public class PlanRepository(AppDbContext context)
    : GenericRepository<Plan>(context), IPlanRepository
{
    public async Task<Plan?> GetByTierAsync(PlanTier tier)
        => await _dbSet
            .FirstOrDefaultAsync(p => p.Name == tier.ToString());
}