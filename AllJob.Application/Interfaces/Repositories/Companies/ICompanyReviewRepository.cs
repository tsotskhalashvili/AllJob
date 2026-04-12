using AllJob.Domain.Entities.Companies;

namespace AllJob.Application.Interfaces.Repositories.Companies;

public interface ICompanyReviewRepository
    : IGenericRepository<CompanyReview>
{
    Task<IReadOnlyList<CompanyReview>> GetByCompanyIdAsync(Guid companyId);
    Task<IReadOnlyList<CompanyReview>> GetPendingAsync();
}