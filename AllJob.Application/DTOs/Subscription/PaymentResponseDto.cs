using AllJob.Domain.Enums;

namespace AllJob.Application.DTOs.Subscription;

public record PaymentResponseDto(
    Guid Id,
    Guid PlanId,
    string PlanName,
    decimal Amount,
    PaymentStatus Status,
    DateTime PaidAt
);
