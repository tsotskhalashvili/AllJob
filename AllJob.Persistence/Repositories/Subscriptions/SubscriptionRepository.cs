using AllJob.Application.Interfaces.Repositories.Subscriptions;
using AllJob.Domain.Entities.Subscriptions;
using AllJob.Persistence.Context;
using AllJob.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Subscriptions;

public class SubscriptionRepository(
    AppDbContext context) : GenericRepository<CompanySubscription>(context), ISubscriptionRepository
{
    public async Task<IReadOnlyList<CompanySubscription>> GetExpiredSubscriptionsAsync()
      => await _dbSet
      
        .Include(s => s.Company)
        .Where(s => s.IsActive && s.EndDate < DateTime.UtcNow)
        .ToListAsync();
}
