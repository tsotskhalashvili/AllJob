namespace AllJob.Application.DTOs.Company;

public record CompanyResponseDto(
    Guid Id,
    string Name,
    string LogoUrl,
    string Website,
    string Industry,
    bool IsVerified,
    DateTime CreatedAt
);
