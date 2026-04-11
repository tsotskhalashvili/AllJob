using AllJob.Application.DTOs.Application;

namespace AllJob.Application.Interfaces.Services.Applications;

public interface IApplicationService
{
    Task<ApplicationResponseDto> CreateAsync(CreateApplicationDto dto, Guid userId);
    Task<IReadOnlyList<ApplicationResponseDto>> GetMyApplicationsAsync(Guid userId);
    Task<IReadOnlyList<ApplicationResponseDto>> GetJobApplicationsAsync(Guid jobId, Guid userId);
    Task<ApplicationResponseDto> UpdateStatusAsync(Guid applicationId, UpdateApplicationStatusDto dto, Guid userId);
}
