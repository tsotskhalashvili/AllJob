namespace AllJob.Application.DTOs.Subscription;

public record PlanResponseDto(
    Guid Id,
    string Name,
    decimal Price,
    int MaxJobListings
);
