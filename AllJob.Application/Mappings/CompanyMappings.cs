using AllJob.Application.DTOs.Company;
using AllJob.Domain.Entities.Companies;

namespace AllJob.Application.Mappings;

public static class CompanyMappings
{
    public static CompanyResponseDto ToDto(this Company company)
       => new(
           Id: company.Id,
           Name: company.Name,
           LogoUrl: company.LogoUrl,
           Website: company.Website,
           FacebookUrl: company.FacebookUrl,
           Description: company.Description,
           Industry: company.Industry,
           IsVerified: company.IsVerified,
           AverageRating: company.Reviews.Any(r => r.IsApproved)
               ? company.Reviews.Where(r => r.IsApproved).Average(r => r.Rating)
               : 0,
           ReviewCount: company.Reviews.Count(r => r.IsApproved),
           CreatedAt: company.CreatedAt
       );

    public static Company ToEntity(this CreateCompanyDto dto, Guid userId)
        => new()
        {
            UserId = userId,
            Name = dto.Name,
            Industry = dto.Industry,
            LogoUrl = dto.LogoUrl,
            Website = dto.Website,
            FacebookUrl = dto.FacebookUrl,
            Description = dto.Description,
            IsVerified = false
        };

    public static void UpdateEntity(this Company company, UpdateCompanyDto dto)
    {
        if (dto.Name is not null) company.Name = dto.Name;
        if (dto.Industry is not null) company.Industry = dto.Industry;
        if (dto.LogoUrl is not null) company.LogoUrl = dto.LogoUrl;
        if (dto.Website is not null) company.Website = dto.Website;
        if (dto.FacebookUrl is not null) company.FacebookUrl = dto.FacebookUrl;
        if (dto.Description is not null) company.Description = dto.Description;
    }
}