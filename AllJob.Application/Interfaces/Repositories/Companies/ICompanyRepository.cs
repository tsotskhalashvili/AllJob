using AllJob.Application.DTOs.Common;
using AllJob.Application.DTOs.Company;
using AllJob.Domain.Entities.Companies;

namespace AllJob.Application.Interfaces.Repositories.Companies;

public interface ICompanyRepository : IGenericRepository<Company>
{
    Task<Company?> GetByUserIdAsync(Guid userId);
    Task<Company?> GetCompanyWithDetailsAsync(Guid id);


    Task<int> GetActiveJobsCountAsync(Guid companyId);

    Task<PagedResponseDto<CompanyResponseDto>> GetPagedCompaniesAsync(CompanyFilterDto filter);
}
