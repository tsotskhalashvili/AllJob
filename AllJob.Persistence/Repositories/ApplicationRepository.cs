using AllJob.Application.Interfaces.Repositories;
using AllJob.Domain.Entities.Applications;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories;

public class ApplicationRepository(AppDbContext context)
    : GenericRepository<JobApplication>(context), IApplicationRepository
{
    public async Task<IReadOnlyList<JobApplication>> GetCandidateApplicationsAsync(Guid userId)
     => await _dbSet
        .AsNoTracking()
        .Include(a => a.Job)
        .ThenInclude(j => j.Company)
        .Where(a => a.UserId == userId)
        .OrderByDescending(a => a.AppliedAt)
        .ToListAsync();

    public async Task<IReadOnlyList<JobApplication>> GetJobApplicationsAsync(
     Guid jobId)
     => await _dbSet
         .AsNoTracking()
         .Include(a => a.Job)
             .ThenInclude(j => j.Company)
         .Include(a => a.User)
         .Where(a => a.JobId == jobId)
         .OrderByDescending(a => a.AppliedAt)
         .ToListAsync();
}
