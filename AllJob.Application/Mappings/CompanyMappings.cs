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
            Industry: company.Industry,
            IsVerified: company.IsVerified,
            CreatedAt: company.CreatedAt
        );

    public static Company ToEntity(this CreateCompanyDto dto, Guid userId)
           => new()
           {
               UserId = userId,
               Name = dto.Name,
               LogoUrl = dto.LogoUrl,
               Website = dto.Website,
               Industry = dto.Industry,
               IsVerified = false
           };

    public static void UpdateEntity(this Company company, UpdateCompanyDto dto)
    {
        company.Name = dto.Name;
        company.LogoUrl = dto.LogoUrl;
        company.Website = dto.Website;
        company.Industry = dto.Industry;
    }


}
