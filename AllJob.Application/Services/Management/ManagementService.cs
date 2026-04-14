using AllJob.Application.DTOs.Management;
using AllJob.Application.Exceptions;
using AllJob.Application.Helpers;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Application.Interfaces.Repositories.Shared;
using AllJob.Application.Interfaces.Repositories.Subscriptions;
using AllJob.Application.Interfaces.Services.Management;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Mappings;
using AllJob.Application.Settings;
using AllJob.Domain.Entities.Auth;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace AllJob.Application.Services.Management;

public class ManagementService(
   IUserRepository userRepository,
    IGenericRepository<Role> roleRepository,
    IGenericRepository<UserRole> userRoleRepository,
    IGenericRepository<AdminProfile> adminProfileRepository,
    IAdminInviteRepository adminInviteRepository,
    IPlanRepository planRepository,
    IStatsRepository statsRepository,
    IEmailService emailService,
    IUnitOfWork unitOfWork,
    IOptions<TokenHashSettings> tokenHashSettings,
    IOptions<AppSettings> appSettings) : IManagementService
{
    private readonly string _secret = tokenHashSettings.Value.Secret;
    private readonly string _baseUrl = appSettings.Value.BaseUrl;
    public async Task InviteAdminAsync(InviteAdminDto dto, Guid superAdminId)
    {
        var existingUser = await userRepository.GetByEmailAsync(dto.Email);
        
        if(existingUser is not null)
            throw new ConflictException($"Email '{dto.Email}' is already registered");

        var rawToken = Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));

        var invite = new AdminInvite { 
        Id = Guid.NewGuid(),
        Email = dto.Email,
        TokenHash = TokenHasher.Hash(rawToken, _secret),
        Role = dto.Role,
        ExpiresAt = DateTime.UtcNow.AddDays(7),
        CreatedByUserId =  superAdminId
        };

        await adminInviteRepository.AddAsync(invite);
        await unitOfWork.SaveChangesAsync();

        await emailService.SendAdminInviteAsync(
            dto.Email,
            rawToken,
            dto.Role.ToString());


    }
     
    public async Task AcceptInviteAsync(AcceptInviteDto dto)
    {
        var tokenHash = TokenHasher.Hash(dto.Token, _secret);


        var invite = await adminInviteRepository
            .GetByTokenHashAsync(tokenHash)
            ?? throw new NotFoundException("Invite", dto.Token);

        var roles = await roleRepository.GetAllAsync();
        var adminRole = roles.FirstOrDefault(r => r.Name == "Admin")
            ?? throw new NotFoundException("Role", "Admin");

        var suffix = Random.Shared.Next(10, 99);
        var corporateEmail = $"{dto.FirstName[0].ToString().ToLower()}" +
            $".{dto.LastName.ToLower()}{suffix}@alljob.ge";

        while (await userRepository.GetByEmailAsync(corporateEmail) is not null)
        {
            suffix = Random.Shared.Next(10, 99);
            corporateEmail = $"{dto.FirstName[0].ToString().ToLower()}" +
                $".{dto.LastName.ToLower()}{suffix}@alljob.ge";
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = corporateEmail,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            IsActive = true,
            IsPasswordChangeRequired = false,
            AdminRole = invite.Role
        };

        await unitOfWork.BeginTransactionAsync();
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            await userRoleRepository.AddAsync(new UserRole
            {
                UserId = user.Id,
                RoleId = adminRole.Id
            });

            await adminProfileRepository.AddAsync(new AdminProfile
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            });

            invite.UsedAt = DateTime.UtcNow;
            adminInviteRepository.Update(invite);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAdminAsync(Guid adminId)
    {
        var admin = await userRepository.GetAdminByIdAsync(adminId)
            ?? throw new NotFoundException("Admin", adminId);

        admin.IsActive = false;
        userRepository.Update(admin);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<AdminResponseDto>> GetAllAdminsAsync()
    {
        var admins = await userRepository.GetAllAdminsAsync();
        return admins.Select(u => u.ToAdminDto()).ToList();
    }
    public async Task<ManagementStatsDto> GetStatsAsync()
    {
        var totalUsers = await statsRepository.GetTotalUsersCountAsync();
        var totalCompanies = await statsRepository.GetTotalCompaniesCountAsync();
        var totalJobs = await statsRepository.GetTotalJobsCountAsync();
        var activeJobs = await statsRepository.GetActiveJobsCountAsync();
        var totalApplications = await statsRepository.GetTotalApplicationsCountAsync();
        var newUsersToday = await statsRepository.GetNewUsersTodayCountAsync();
        var newJobsToday = await statsRepository.GetNewJobsTodayCountAsync();

        return new ManagementStatsDto(
            TotalUsers: totalUsers,
            TotalCompanies: totalCompanies,
            TotalJobs: totalJobs,
            ActiveJobs: activeJobs,
            TotalApplications: totalApplications,
            NewUsersToday: newUsersToday,
            NewJobsToday: newJobsToday
        );
    }


    public async Task UpdateAdminRoleAsync(Guid adminId, UpdateAdminRoleDto dto)
    {
        var admin = await userRepository.GetAdminByIdAsync(adminId)
            ?? throw new NotFoundException("Admin", adminId);

        admin.AdminRole = dto.Role;
        userRepository.Update(admin);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdatePlanAsync(Guid planId, UpdatePlanDto dto)
    {
        var plan = await planRepository.GetByIdAsync(planId)
            ?? throw new NotFoundException("Plan", planId);

        plan.UpdateEntity(dto);
        planRepository.Update(plan);
        await unitOfWork.SaveChangesAsync();
    }
}
