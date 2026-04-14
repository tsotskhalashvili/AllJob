namespace AllJob.Application.DTOs.Management;

public record UpdatePlanDto(
    decimal? Price,
    int? MaxJobListings
);