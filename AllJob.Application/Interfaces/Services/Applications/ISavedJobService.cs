using AllJob.Application.DTOs.Job;

namespace AllJob.Application.Interfaces.Services.Applications;

public interface ISavedJobService
{
    Task SaveJobAsync(Guid jobId, Guid userId);
    Task UnsaveJobAsync(Guid jobId, Guid userId);
    Task<IReadOnlyList<JobResponseDto>> GetMySavedJobsAsync(Guid userId);
}