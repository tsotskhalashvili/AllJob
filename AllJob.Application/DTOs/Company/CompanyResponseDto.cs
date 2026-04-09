namespace AllJob.Application.DTOs.Company;

public record CompanyResponseDto(
    Guid Id,
    string Name,
    string? LogoUrl,
    string? Website,
    string? FacebookUrl,
    string? Description,
    string Industry,
    bool IsVerified,
    DateTime CreatedAt
);