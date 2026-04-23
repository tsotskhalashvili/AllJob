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
        var result = await messageService.GetMessagesAsync(conversationId, userId);
        return Ok(result);
    }

    [HttpPost("conversation")]
    public async Task<IActionResult> GetOrCreateConversation(
        [FromBody] CreateConversationDto dto)
    {
        var result = await messageService
            .GetOrCreateConversationAsync(dto.CandidateId, dto.EmployerId);
        return Ok(result);
    }
}