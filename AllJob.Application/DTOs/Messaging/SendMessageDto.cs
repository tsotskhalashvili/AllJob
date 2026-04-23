namespace AllJob.Application.DTOs.Messaging;

public record SendMessageDto(
    Guid ConversationId,
    string Content
);