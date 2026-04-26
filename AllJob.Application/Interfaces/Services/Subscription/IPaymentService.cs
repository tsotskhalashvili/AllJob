using AllJob.Application.DTOs.Payment;

namespace AllJob.Application.Interfaces.Services.Subscription;

public interface IPaymentService
{
    Task<InitiatePaymentResponseDto> InitiatePaymentAsync(InitiatePaymentDto dto, Guid userId);
    Task HandleWebhookAsync(BogWebhookDto dto);
}