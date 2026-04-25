using AllJob.Application.DTOs.Admin;
using AllJob.Application.DTOs.Common;
using AllJob.Application.DTOs.Company;

namespace AllJob.Application.Interfaces.Services.Admin;

public interface IEmployerManagerService
{
    Task<PagedResponseDto<UserResponseDto>> GetAllEmployersAsync(int page, int pageSize);
    Task DeactivateEmployerAsync(Guid userId);
    Task DeleteEmployerAsync(Guid userId);
    Task<IReadOnlyList<CompanyResponseDto>> GetAllCompaniesAsync();
    Task VerifyCompanyAsync(Guid companyId);
    Task RejectCompanyAsync(Guid companyId);
}