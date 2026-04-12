using AllJob.Application.Interfaces.Repositories.Companies;
using AllJob.Domain.Entities.Companies;
using AllJob.Persistence.Context;
using AllJob.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Companies;

public class CompanyReviewRepository(AppDbContext context)
    : GenericRepository<CompanyReview>(context), ICompanyReviewRepository
{
    public async Task<IReadOnlyList<CompanyReview>> GetByCompanyIdAsync(Guid companyId)
        => await _dbSet
            .AsNoTracking()
            .Include(r => r.User)
            .Where(r => r.CompanyId == companyId && r.IsApproved)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyList<CompanyReview>> GetPendingAsync()
        => await _dbSet
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.Company)
            .Where(r => !r.IsApproved)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
}