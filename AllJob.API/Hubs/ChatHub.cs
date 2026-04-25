using AllJob.Application.DTOs.Messaging;
using AllJob.Application.Interfaces.Services.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace AllJob.API.Hubs;

[Authorize]
public class ChatHub(IMessageService messageService) : Hub
{
    public async Task SendMessage(Guid conversationId, string content)
    {
        var senderId = Guid.Parse(
            Context.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var message = await messageService.SendMessageAsync(senderId, new SendMessageDto(
            ConversationId: conversationId,
            Content: content
        ));

        await Clients.Group(conversationId.ToString())
            .SendAsync("ReceiveMessage", message);
    }

    public async Task JoinConversation(Guid conversationId)
    {
        var userId = Guid.Parse(
            Context.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var isParticipant = await messageService.IsParticipantAsync(userId, conversationId);
        if (!isParticipant)
            throw new HubException("Access denied");

        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            conversationId.ToString());
    }

    public async Task LeaveConversation(Guid conversationId)
    {
        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            conversationId.ToString());
    }
}