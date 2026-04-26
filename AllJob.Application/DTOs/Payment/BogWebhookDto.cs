namespace AllJob.Application.DTOs.Payment;

public record BogWebhookDto(
    string TransactionId,
    Guid PaymentId,
    string Status
);