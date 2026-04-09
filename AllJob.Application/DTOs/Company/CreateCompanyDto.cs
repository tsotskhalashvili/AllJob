namespace AllJob.Application.DTOs.Company;

public record CreateCompanyDto(
    string Name,
    string Industry,
    string? LogoUrl,
    string? Website,
    string? FacebookUrl,
    string? Description
);
