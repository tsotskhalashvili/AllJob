using AllJob.Domain.Entities.Companies;

namespace AllJob.Application.Interfaces.Repositories;

public interface ICompanyRepository : IGenericRepository<Company>
{
    Task<Company?> GetCompanyWithDetailsAsync(Guid id);

    Task<int> GetActiveJobsCountAsync(Guid companyId);
}
