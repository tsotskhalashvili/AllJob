using AllJob.Application.Interfaces.Repositories.Messaging;
using AllJob.Domain.Entities.Messaging;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Messaging;

public class MessageRepository(AppDbContext context) : IMessageRepository
{
    public async Task<Conversation?> GetConversationAsync(Guid candidateId, Guid employerId)
        => await context.Conversations
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c =>
                c.CandidateId == candidateId &&
                c.EmployerId == employerId);
    public async Task AddConversationAsync(Conversation conversation)
      => await context.Conversations.AddAsync(conversation);
    public async Task AddMessageAsync(Message message)
      => await context.Messages.AddAsync(message);

    public async Task<IReadOnlyList<Message>> GetMessagesAsync(Guid conversationId)
       => await context.Messages
           .AsNoTracking()
           .Where(m => m.ConversationId == conversationId)
           .OrderBy(m => m.CreatedAt)
           .ToListAsync();

    public async Task<IReadOnlyList<Conversation>> GetUserConversationsAsync(Guid userId)
        => await context.Conversations
            .AsNoTracking()
            .Include(c => c.Messages.OrderByDescending(m => m.CreatedAt).Take(1))
            .Include(c => c.Candidate)
                .ThenInclude(u => u.CandidateProfile)
            .Include(c => c.Employer)
            .Where(c => c.CandidateId == userId || c.EmployerId == userId)
            .OrderByDescending(c => c.LastMessageAt)
            .ToListAsync();

    public async Task<Conversation?> GetConversationByIdAsync(Guid conversationId)
    => await context.Conversations
        .FirstOrDefaultAsync(c => c.Id == conversationId);

    public async Task<bool> IsParticipantAsync(Guid userId, Guid conversationId)
     => await context.Conversations
       .AnyAsync(c => c.Id == conversationId &&
        (c.CandidateId == userId || c.EmployerId == userId));
}
