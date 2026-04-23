namespace AllJob.Application.DTOs.Messaging;

public record MessageResponseDto(
    Guid Id,
    Guid ConversationId,
    Guid SenderId,
    string Content,
    bool IsRead,
    DateTime CreatedAt
);