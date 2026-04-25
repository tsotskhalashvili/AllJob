using AllJob.Application.DTOs.Admin;
using AllJob.Application.DTOs.Common;

namespace AllJob.Application.Interfaces.Services.Admin;

public interface ICandidateManagerService
{
    Task<PagedResponseDto<UserResponseDto>> GetAllUsersAsync(int page, int pageSize);
    Task DeactivateUserAsync(Guid userId);
    Task DeleteUserAsync(Guid userId);
}