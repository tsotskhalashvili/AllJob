using AllJob.Application.Interfaces.Repositories;
using AllJob.Domain.Entities.Applications;
using AllJob.Domain.Entities.Jobs;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories;

public class SavedJobRepository(AppDbContext context) : ISavedJobRepository
{
    public async Task<SavedJob?> GetAsync(Guid userId, Guid jobId)
        => await context.SavedJobs
            .FirstOrDefaultAsync(s => s.UserId == userId && s.JobId == jobId);

    public async Task<IReadOnlyList<Job>> GetSavedJobsAsync(Guid userId)
        => await context.SavedJobs
            .AsNoTracking()
            .Where(s => s.UserId == userId)
            .Include(s => s.Job)
                .ThenInclude(j => j.Company)
            .Include(s => s.Job)
                .ThenInclude(j => j.Category)
            .Include(s => s.Job)
                .ThenInclude(j => j.Address)
            .Include(s => s.Job)
                .ThenInclude(j => j.JobSkills)
                    .ThenInclude(js => js.Skill)
            .Select(s => s.Job)
            .ToListAsync();

    public async Task AddAsync(SavedJob savedJob)
        => await context.SavedJobs.AddAsync(savedJob);

    public void Remove(SavedJob savedJob)
        => context.SavedJobs.Remove(savedJob);
}