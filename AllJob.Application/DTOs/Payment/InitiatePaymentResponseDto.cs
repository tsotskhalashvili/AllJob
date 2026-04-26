namespace AllJob.Application.DTOs.Payment;

public record InitiatePaymentResponseDto(
    Guid PaymentId,
    string PaymentUrl,
    decimal Amount,
    string Status
);