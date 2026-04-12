using AllJob.Domain.Entities.Notifications;

namespace AllJob.Application.Interfaces.Repositories.Notifications;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<IReadOnlyList<Notification>> GetByUserIdAsync(Guid userId);
    Task MarkAllAsReadAsync(Guid userId);

}