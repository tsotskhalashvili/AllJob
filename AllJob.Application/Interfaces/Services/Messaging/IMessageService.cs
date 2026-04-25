using AllJob.Application.DTOs.Messaging;

namespace AllJob.Application.Interfaces.Services.Messaging;

public interface IMessageService
{
    Task<MessageResponseDto> SendMessageAsync(Guid senderId, SendMessageDto dto);
    Task<IReadOnlyList<MessageResponseDto>> GetMessagesAsync(Guid conversationId, Guid userId);
    Task<IReadOnlyList<ConversationResponseDto>> GetConversationsAsync(Guid userId);
    Task<ConversationResponseDto> GetOrCreateConversationAsync(Guid currentUserId, Guid otherUserId);
    Task<bool> IsParticipantAsync(Guid userId, Guid conversationId);
}