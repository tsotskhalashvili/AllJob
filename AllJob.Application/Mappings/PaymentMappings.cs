using AllJob.Application.DTOs.Payment;
using AllJob.Domain.Entities.Subscriptions;

namespace AllJob.Application.Mappings;

public static class PaymentMappings
{
    public static InitiatePaymentResponseDto ToPaymentDto(this Payment payment)
        => new(
            PaymentId: payment.Id,
            PaymentUrl: $"https://bog.mock/pay/{payment.Id}",
            Amount:payment.Amount,
            Status: payment.Status.ToString());
}
