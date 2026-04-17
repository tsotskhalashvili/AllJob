using AllJob.Application.DTOs.Common;
using AllJob.Application.DTOs.Job;
using AllJob.Domain.Entities.Jobs;

namespace AllJob.Application.Interfaces.Repositories.Jobs;

public interface IJobRepository : IGenericRepository<Job>
{
    Task<PagedResponseDto<JobResponseDto>> GetPagedJobsAsync(JobFilterDto filter);

    Task<Job?> GetJobWithDetailsAsync(Guid id);

    Task<IReadOnlyList<Job>> GetExpiredJobsAsync();
    Task<IReadOnlyList<Job>> GetRecentJobsAsync(int hours);
   

}
