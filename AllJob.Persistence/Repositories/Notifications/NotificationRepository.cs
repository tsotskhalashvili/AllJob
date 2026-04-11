using AllJob.Application.Interfaces.Repositories.Notifications;
using AllJob.Domain.Entities.Notifications;
using AllJob.Persistence.Context;
using AllJob.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Notifications;

public class NotificationRepository(AppDbContext context)
    : GenericRepository<Notification>(context), INotificationRepository
{
    public async Task<IReadOnlyList<Notification>> GetByUserIdAsync(Guid userId)
        => await _dbSet
            .AsNoTracking()
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
}