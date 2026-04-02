using AllJob.Application.DTOs.Notification;

namespace AllJob.Application.Interfaces.Services;

public interface INotificationService
{
    Task<IReadOnlyList<NotificationResponseDto>> GetMyNotificationsAsync(Guid userId);
    Task MarkAsReadAsync(Guid notificationId, Guid userId);
} 