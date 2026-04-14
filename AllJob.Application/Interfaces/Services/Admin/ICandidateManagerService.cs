using AllJob.Application.DTOs.Admin;

namespace AllJob.Application.Interfaces.Services.Admin;

public interface ICandidateManagerService
{
    Task<IReadOnlyList<UserResponseDto>> GetAllUsersAsync();
    Task DeactivateUserAsync(Guid userId);
    Task DeleteUserAsync(Guid userId);
}