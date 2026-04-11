using AllJob.Application.DTOs.Common;
using AllJob.Application.DTOs.Company;

namespace AllJob.Application.Interfaces.Services.Company;

public interface ICompanyService
{
    Task<CompanyResponseDto> GetCompanyByIdAsync(Guid id);
    Task<CompanyResponseDto> CreateCompanyAsync(CreateCompanyDto dto, Guid userId);
    Task UpdateCompanyAsync(Guid id, UpdateCompanyDto dto, Guid userId);
    Task DeleteCompanyAsync(Guid id, Guid userId);
    Task<PagedResponseDto<CompanyResponseDto>> GetCompaniesAsync(CompanyFilterDto filter);
}