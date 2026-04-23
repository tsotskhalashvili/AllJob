using AllJob.Application.DTOs.Messaging;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Messaging;
using AllJob.Application.Interfaces.Services.Messaging;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Messaging;

namespace AllJob.Application.Services.Messaging;

public class MessageService(
    IMessageRepository messageRepository,
    IUnitOfWork unitOfWork) : IMessageService
{
    public async Task<MessageResponseDto> SendMessageAsync(Guid senderId, SendMessageDto dto)
    {
        var conversation = await messageRepository
            .GetConversationByIdAsync(dto.ConversationId)
            ?? throw new NotFoundException("Conversation", dto.ConversationId);

        var message = new Message
        {
            Id = Guid.NewGuid(),
            ConversationId = dto.ConversationId,
            SenderId = senderId,
            Content = dto.Content,
            IsRead = false
        };

        conversation.LastMessageAt = DateTime.UtcNow;

        await messageRepository.AddMessageAsync(message);
        await unitOfWork.SaveChangesAsync();

        return message.ToDto();
    }

    public async Task<IReadOnlyList<MessageResponseDto>> GetMessagesAsync(
        Guid conversationId, Guid userId)
    {
        var messages = await messageRepository.GetMessagesAsync(conversationId);
        return messages.Select(m => m.ToDto()).ToList();
    }

    public async Task<IReadOnlyList<ConversationResponseDto>> GetConversationsAsync(Guid userId)
    {
        var conversations = await messageRepository.GetUserConversationsAsync(userId);
        return conversations.Select(c => c.ToDto()).ToList();
    }

    public async Task<ConversationResponseDto> GetOrCreateConversationAsync(
        Guid candidateId, Guid employerId)
    {
        var conversation = await messageRepository
            .GetConversationAsync(candidateId, employerId);

        if (conversation is null)
        {
            conversation = new Conversation
            {
                Id = Guid.NewGuid(),
                CandidateId = candidateId,
                EmployerId = employerId,
                LastMessageAt = DateTime.UtcNow
            };

            await messageRepository.AddConversationAsync(conversation);
            await unitOfWork.SaveChangesAsync();
        }

        return conversation.ToDto();
    }
}