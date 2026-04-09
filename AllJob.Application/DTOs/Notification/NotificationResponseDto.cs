using AllJob.Domain.Enums.Notifications;

namespace AllJob.Application.DTOs.Notification;

public record NotificationResponseDto(
     Guid Id,
    string Title,
    string Message,
    NotificationType Type,
    string? ActionUrl,
    bool IsRead,
    DateTime CreatedAt
);
