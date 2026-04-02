using AllJob.Domain.Entities.Applications;
using AllJob.Domain.Entities.Jobs;

namespace AllJob.Application.Interfaces.Repositories;

public interface ISavedJobRepository
{
    Task<SavedJob?> GetAsync(Guid userId, Guid jobId);
    Task<IReadOnlyList<Job>> GetSavedJobsAsync(Guid userId);
    Task AddAsync(SavedJob savedJob);
    void Remove(SavedJob savedJob);
}