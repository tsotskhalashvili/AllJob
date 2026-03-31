using AllJob.Application.DTOs.Notification;
using AllJob.Domain.Entities.Notifications;

namespace AllJob.Application.Mappings;

public static class NotificationMappings
{
    public static NotificationResponseDto ToDto(this Notification notification)
        => new(
            Id: notification.Id,
            Title: notification.Title,
            Message: notification.Message,
            Type: notification.Type,
            IsRead: notification.IsRead,
            CreatedAt: notification.CreatedAt
        );
}