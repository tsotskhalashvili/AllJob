using AllJob.Application.DTOs.Management;

namespace AllJob.Application.Interfaces.Services.Management;

public interface IManagementService
{
    Task InviteAdminAsync(InviteAdminDto dto, Guid superAdminId);
    Task AcceptInviteAsync(AcceptInviteDto dto); 
    Task<IReadOnlyList<AdminResponseDto>> GetAllAdminsAsync();

    Task DeleteAdminAsync(Guid adminId);
    Task UpdateAdminRoleAsync(Guid adminId, UpdateAdminRoleDto dto);
    Task UpdatePlanAsync(Guid planId, UpdatePlanDto dto);
    Task<ManagementStatsDto> GetStatsAsync();

}
    