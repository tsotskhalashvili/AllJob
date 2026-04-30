using AllJob.Application.DTOs.Messaging;
using AllJob.Application.Interfaces.Services.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessageController(IMessageService messageService) : BaseController
{
    [HttpGet("conversations")]
    public async Task<IActionResult> GetConversations()
    {
        var userId = Guid.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await messageService.GetConversationsAsync(userId);
        return Ok(result);
    }

    [HttpGet("{conversationId}")]
    public async Task<IActionResult> GetMessages(Guid conversationId)
    {
        var userId = Guid.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var isParticipant = await messageService.IsParticipantAsync(userId, conversationId);
        if (!isParticipant)
            return Forbid();

        var result = await messageService.GetMessagesAsync(conversationId, userId);
        return Ok(result);
    }

    [HttpPost("conversation")]
    public async Task<IActionResult> GetOrCreateConversation(
        [FromBody] CreateConversationDto dto)
    {
        var currentUserId = Guid.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await messageService
            .GetOrCreateConversationAsync(currentUserId, dto.OtherUserId);
        return Ok(result);
    }

    [HttpPost("send")] 
    public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
    {
       
        var senderId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var isParticipant = await messageService.IsParticipantAsync(senderId, dto.ConversationId);
        if (!isParticipant)
            return Forbid();

      
        var result = await messageService.SendMessageAsync(senderId, dto);

        return Ok(result);
    }
}