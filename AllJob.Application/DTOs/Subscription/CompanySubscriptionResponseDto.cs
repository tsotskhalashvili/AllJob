namespace AllJob.Application.DTOs.Subscription;

public record CompanySubscriptionResponseDto(
    Guid Id,
    string PlanName,
    decimal PlanPrice,
    DateTime StartDate,
    DateTime EndDate,
    bool IsActive
);
