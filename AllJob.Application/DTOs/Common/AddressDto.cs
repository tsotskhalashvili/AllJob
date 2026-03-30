namespace AllJob.Application.DTOs.Common;

public record AddressDto(
    Guid Id,
    string Country,
    string City
);