using AllJob.Application.DTOs.Subscription;
using AllJob.Domain.Entities.Subscriptions;

namespace AllJob.Application.Mappings;

public static class SubscriptionMappings
{
    public static PlanResponseDto ToDto(this Plan plan)
        => new(
            Id: plan.Id,
            Name: plan.Name,
            Price: plan.Price,
            MaxJobListings: plan.MaxJobListings
        );

    public static PaymentResponseDto ToDto(this Payment payment)
        => new(
            Id: payment.Id,
            PlanId: payment.PlanId,
            PlanName: payment.Plan.Name,
            Amount: payment.Amount,
            Status: payment.Status,
            PaidAt: payment.PaidAt
        );

    public static CompanySubscriptionResponseDto ToDto(
        this CompanySubscription subscription)
        => new(
            Id: subscription.Id,
            PlanName: subscription.Plan.Name,
            PlanPrice: subscription.Plan.Price,
            StartDate: subscription.StartDate,
            EndDate: subscription.EndDate,
            IsActive: subscription.IsActive
        );
}