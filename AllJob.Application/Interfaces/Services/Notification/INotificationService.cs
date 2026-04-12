using AllJob.Application.DTOs.Notification;

namespace AllJob.Application.Interfaces.Services.Notification;

public interface INotificationService
{
    Task<IReadOnlyList<NotificationResponseDto>> GetMyNotificationsAsync(Guid userId);
    Task MarkAsReadAsync(Guid notificationId, Guid userId);
    Task DeleteAsync(Guid notificationId, Guid userId);
    Task MarkAllAsReadAsync(Guid userId);
} 