using AllJob.Application.DTOs.Admin;

namespace AllJob.Application.Interfaces.Services;

public interface IAdminService
{
    Task<IReadOnlyList<UserResponseDto>> GetAllUsersAsync();
    Task DeactivateUserAsync(Guid userId);
    Task VerifyCompanyAsync(Guid companyId);
}