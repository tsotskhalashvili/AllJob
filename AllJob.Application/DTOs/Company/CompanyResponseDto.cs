namespace AllJob.Application.DTOs.Company;

public record CompanyResponseDto(
    Guid Id,
    string Name,
    string? LogoUrl,
     Guid OwnerUserId, //add for messing 
    string? Website,
    string? FacebookUrl,
    string? Description,
    string Industry,
    bool IsVerified,
    double AverageRating,  
    int ReviewCount,
    DateTime CreatedAt,
    string Tier
);