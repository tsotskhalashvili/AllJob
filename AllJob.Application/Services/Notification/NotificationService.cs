using AllJob.Application.DTOs.Notification;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Notifications;
using AllJob.Application.Interfaces.Services.Notification;
using AllJob.Application.Mappings;

namespace AllJob.Application.Services.Notification;

public class NotificationService(
    INotificationRepository notificationRepository,
    IUnitOfWork unitOfWork
    ) : INotificationService
{
    public async Task<IReadOnlyList<NotificationResponseDto>> GetMyNotificationsAsync(Guid userId)
    {
        var notifications = await notificationRepository
             .GetByUserIdAsync(userId);

        return notifications.Select(n => n.ToDto()).ToList();
    }

    public async Task MarkAsReadAsync(Guid notificationId, Guid userId)
    {
        var notification = await notificationRepository
              .GetByIdAsync(notificationId)
                ?? throw new NotFoundException("Notification", notificationId);

        if (notification.UserId != userId)
            throw new ForbiddenException();

        notification.IsRead = true;
        notificationRepository.Update(notification);
        await unitOfWork.SaveChangesAsync();
    }
}
