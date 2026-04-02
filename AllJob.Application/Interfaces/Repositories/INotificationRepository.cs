using AllJob.Domain.Entities.Notifications;

namespace AllJob.Application.Interfaces.Repositories;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<IReadOnlyList<Notification>> GetByUserIdAsync(Guid userId);
   
}