using AllJob.Application.DTOs.Management;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Application.Interfaces.Repositories.Shared;
using AllJob.Application.Interfaces.Repositories.Subscriptions;
using AllJob.Application.Interfaces.Services.Management;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Domain.Entities.Auth;

namespace AllJob.Application.Services.Management;

public class ManagementService(
    IUserRepository userRepository,
    IGenericRepository<Role> roleRepository,
    IPlanRepository planRepository,
    IStatsRepository statsRepository,
    IEmailService emailService,
    IUnitOfWork unitOfWork) : IManagementService
{
    public Task DeleteAdminAsync(Guid adminId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<AdminResponseDto>> GetAllAdminsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ManagementStatsDto> GetStatsAsync()
    {
        throw new NotImplementedException();
    }

    public Task InviteAdminAsync(InviteAdminDto dto, Guid superAdminId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAdminRoleAsync(Guid adminId, UpdateAdminRoleDto dto)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePlanAsync(Guid planId, UpdatePlanDto dto)
    {
        throw new NotImplementedException();
    }
}
