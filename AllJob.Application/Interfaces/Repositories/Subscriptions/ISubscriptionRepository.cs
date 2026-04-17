using AllJob.Domain.Entities.Subscriptions;

namespace AllJob.Application.Interfaces.Repositories.Subscriptions;

public interface ISubscriptionRepository : IGenericRepository<CompanySubscription>
{
    Task<IReadOnlyList<CompanySubscription>> GetExpiredSubscriptionsAsync();
}
