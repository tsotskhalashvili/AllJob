using AllJob.Application.DTOs.Notification;
using AllJob.Domain.Enums.Notifications;

namespace AllJob.Application.Interfaces.Services.Notification;

public interface INotificationService
{
    Task<IReadOnlyList<NotificationResponseDto>> GetMyNotificationsAsync(Guid userId);
    Task MarkAsReadAsync(Guid notificationId, Guid userId);
    Task DeleteAsync(Guid notificationId, Guid userId);
    Task MarkAllAsReadAsync(Guid userId);
    Task CreateAsync(Guid userId, string title, string message, NotificationType type, string? actionUrl = null);
} 