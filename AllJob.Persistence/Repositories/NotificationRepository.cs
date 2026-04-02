using AllJob.Application.Interfaces.Repositories;
using AllJob.Domain.Entities.Notifications;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories;

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