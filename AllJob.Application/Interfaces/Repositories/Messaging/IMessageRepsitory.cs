using AllJob.Domain.Entities.Messaging;

namespace AllJob.Application.Interfaces.Repositories.Messaging;

public interface IMessageRepository
{
    Task<Conversation?> GetConversationAsync(Guid candidateId, Guid employerId);
    Task AddConversationAsync(Conversation conversation);
    Task AddMessageAsync(Message message);
    Task<IReadOnlyList<Message>> GetMessagesAsync(Guid conversationId);
    Task<IReadOnlyList<Conversation>> GetUserConversationsAsync(Guid userId);
    Task<Conversation?> GetConversationByIdAsync(Guid conversationId);
}