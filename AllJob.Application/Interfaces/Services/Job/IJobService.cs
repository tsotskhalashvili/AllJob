using AllJob.Application.DTOs.Common;
using AllJob.Application.DTOs.Job;

namespace AllJob.Application.Interfaces.Services.Job;

public interface IJobService
{
    Task<PagedResponseDto<JobResponseDto>> GetJobsAsync(JobFilterDto filter);
    Task<JobResponseDto> GetJobByIdAsync(Guid id);
    Task<JobResponseDto> CreateJobAsync(CreateJobDto dto, Guid userId);
    Task UpdateJobAsync(Guid id, UpdateJobDto dto, Guid userId);
    Task DeleteJobAsync(Guid id, Guid userId);

    Task<int> GetApplicationsCountAsync(Guid jobId, Guid userId);
}
