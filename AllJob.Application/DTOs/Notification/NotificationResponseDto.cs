using AllJob.Domain.Enums;

namespace AllJob.Application.DTOs.Notification;

public record NotificationResponseDto(
    Guid Id,
    string Title,
    string Message,
    NotificationType Type,
    bool IsRead,
    DateTime CreatedAt
);
