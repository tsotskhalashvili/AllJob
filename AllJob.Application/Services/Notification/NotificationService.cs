using AllJob.Application.DTOs.Notification;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Notifications;
using AllJob.Application.Interfaces.Services.Notification;
using AllJob.Application.Mappings;
using NotificationEntity = AllJob.Domain.Entities.Notifications.Notification; 
using AllJob.Domain.Enums.Notifications; 

namespace AllJob.Application.Services.Notification;

public class NotificationService(
    INotificationRepository notificationRepository,
    IUnitOfWork unitOfWork
    ) : INotificationService
{
    public async Task CreateAsync(Guid userId, 
        string title, 
        string message, 
        NotificationType type, 
        string? actionUrl = null)
    {
        var notification = new NotificationEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = title,
            Message = message,
            Type = type,
            ActionUrl = actionUrl,
            IsRead = false
        };
        await notificationRepository.AddAsync(notification);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid notificationId, Guid userId)
    {
        var notification = await notificationRepository
            .GetByIdAsync(notificationId)
                ?? throw new NotFoundException("Notification", notificationId);

        if (notification.UserId != userId)
            throw new ForbiddenException();

        notificationRepository.Delete(notification);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<NotificationResponseDto>> GetMyNotificationsAsync(Guid userId)
    {
        var notifications = await notificationRepository
             .GetByUserIdAsync(userId);

        return notifications.Select(n => n.ToDto()).ToList();
    }

    public async Task MarkAllAsReadAsync(Guid userId)
    {
        await notificationRepository.MarkAllAsReadAsync(userId);
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
