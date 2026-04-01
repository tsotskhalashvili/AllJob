using AllJob.Application.DTOs.Common;
using AllJob.Application.DTOs.Job;

namespace AllJob.Application.Interfaces.Services;

public interface IJobService
{
    Task<PagedResponseDto<JobResponseDto>> GetJobsAsync(JobFilterDto filter);
    Task<JobResponseDto> GetJobByIdAsync(Guid id);
    Task<JobResponseDto> CreateJobAsync(CreateJobDto dto);
    Task UpdateJobAsync(Guid id, UpdateJobDto dto, Guid companyId);
    Task DeleteJobAsync(Guid id, Guid companyId);
}
