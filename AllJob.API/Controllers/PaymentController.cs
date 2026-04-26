using AllJob.Application.DTOs.Payment;
using AllJob.Application.Interfaces.Services.Subscription;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController(
    IPaymentService paymentService) : BaseController
{
    [HttpPost("initiate")]
    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> InitiatePayment(
        [FromBody] InitiatePaymentDto dto)
    {
        var userId = Guid.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await paymentService.InitiatePaymentAsync(dto, userId);
        return Ok(result);
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook(
        [FromBody] BogWebhookDto dto)
    {
        await paymentService.HandleWebhookAsync(dto);
        return Ok();
    }
}