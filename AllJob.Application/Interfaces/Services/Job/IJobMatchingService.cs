using AllJob.Application.DTOs.Job;

namespace AllJob.Application.Interfaces.Services.Job;

public interface IJobMatchingService
{
    Task<IReadOnlyList<JobResponseDto>> GetRecommendedJobsAsync(Guid userId);
    Task<int> GetJobMatchScoreAsync(Guid userId, Guid jobId);
}
