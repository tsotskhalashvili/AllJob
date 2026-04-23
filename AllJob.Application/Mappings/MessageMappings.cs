using AllJob.Application.DTOs.Messaging;
using AllJob.Domain.Entities.Messaging;

namespace AllJob.Application.Mappings;

public static class MessageMappings
{
    public static MessageResponseDto ToDto(this Message message)
        => new(
            Id: message.Id,
            ConversationId: message.ConversationId,
            SenderId: message.SenderId,
            Content: message.Content,
            IsRead: message.IsRead,
            CreatedAt: message.CreatedAt
        );

    public static ConversationResponseDto ToDto(this Conversation conversation)
        => new(
            Id: conversation.Id,
            CandidateId: conversation.CandidateId,
            EmployerId: conversation.EmployerId,
            LastMessageAt: conversation.LastMessageAt,
            LastMessage: conversation.Messages
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefault()?.ToDto()
        );
}