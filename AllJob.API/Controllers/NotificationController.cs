using AllJob.Application.Interfaces.Services.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NotificationController(
    INotificationService notificationService) : BaseController
{
    private Guid UserId => Guid.Parse(
        User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpGet]
    public async Task<IActionResult> GetMyNotifications()
    {
        var result = await notificationService
            .GetMyNotificationsAsync(UserId);
        return Ok(result);
    }

    [HttpPatch("{id}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        await notificationService.MarkAsReadAsync(id, UserId);
        return NoContent();
    }
}