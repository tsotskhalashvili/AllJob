using AllJob.Application.Interfaces.Repositories;
using AllJob.Domain.Entities.Subscriptions;
using AllJob.Domain.Enums.Subscriptions;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories;

public class PlanRepository(AppDbContext context)
    : GenericRepository<Plan>(context), IPlanRepository
{
    public async Task<Plan?> GetByTierAsync(PlanTier tier)
        => await _dbSet
            .FirstOrDefaultAsync(p => p.Name == tier.ToString());
}