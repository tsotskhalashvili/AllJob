using AllJob.Application.Interfaces.Repositories.Applications;
using AllJob.Domain.Entities.Applications;
using AllJob.Persistence.Context;
using AllJob.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Applications;

public class ApplicationRepository(AppDbContext context)
    : GenericRepository<JobApplication>(context), IApplicationRepository
{
    public async Task<IReadOnlyList<JobApplication>> GetCandidateApplicationsAsync(Guid userId)
     => await _dbSet
        .AsNoTracking()
        .Include(a => a.Job)
        .ThenInclude(j => j.Company)
        .Where(a => a.UserId == userId)
        
        .ToListAsync();

    public async Task<IReadOnlyList<JobApplication>> GetJobApplicationsAsync(
     Guid jobId)
     => await _dbSet
         .AsNoTracking()
         .Include(a => a.Job)
             .ThenInclude(j => j.Company)
         .Include(a => a.User)
         .Where(a => a.JobId == jobId)
         
         .ToListAsync();
}
