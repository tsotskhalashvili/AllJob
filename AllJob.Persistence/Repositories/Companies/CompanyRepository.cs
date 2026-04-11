using AllJob.Application.Interfaces.Repositories.Companies;
using AllJob.Domain.Entities.Companies;
using AllJob.Domain.Enums.Jobs;
using AllJob.Persistence.Context;
using AllJob.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Companies;

public class CompanyRepository(AppDbContext context)
    : GenericRepository<Company>(context), ICompanyRepository
{
    public async Task<Company?> GetCompanyWithDetailsAsync(Guid id)
        => await _dbSet
            .AsNoTracking()
            .Include(c => c.Jobs)
            .Include(c => c.Reviews)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<int> GetActiveJobsCountAsync(Guid companyId)
        => await _dbSet
            .AsNoTracking()
            .Where(c => c.Id == companyId)
            .SelectMany(c => c.Jobs)
            .CountAsync(j => j.Status == JobStatus.Active);
}