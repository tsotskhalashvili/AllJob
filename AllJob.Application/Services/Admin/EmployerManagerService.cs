using AllJob.Application.Constants;
using AllJob.Application.DTOs.Admin;
using AllJob.Application.DTOs.Common;
using AllJob.Application.DTOs.Company;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Application.Interfaces.Repositories.Companies;
using AllJob.Application.Interfaces.Services.Admin;
using AllJob.Application.Interfaces.Services.Notification;
using AllJob.Application.Mappings;
using AllJob.Domain.Enums.Notifications;
using AllJob.Domain.Enums.Subscriptions;

namespace AllJob.Application.Services.Admin;

public class EmployerManagerService(
    IUserRepository userRepository,
    ICompanyRepository companyRepository,
    INotificationService notificationService,
    IUnitOfWork unitOfWork) : IEmployerManagerService
{
    public async Task<PagedResponseDto<UserResponseDto>> GetAllEmployersAsync(int page, int pageSize)
    {
        var result = await userRepository.GetAllEmployersAsync(page, pageSize);
        var items = result.Items.Select(u => u.ToDto()).ToList();
        return new PagedResponseDto<UserResponseDto>(items, result.TotalCount, page, pageSize);
    }
    public async Task DeactivateEmployerAsync(Guid userId)
    {
        var user = await userRepository.GetByIdAsync(userId)
            ?? throw new NotFoundException("User", userId);

        user.IsActive = false;
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteEmployerAsync(Guid userId)
    {
        var user = await userRepository.GetByIdWithRolesAsync(userId)
            ?? throw new NotFoundException("User", userId);

        var role = user.UserRoles.FirstOrDefault()?.Role.Name;
        if (role is "Admin" or "SuperAdmin")
            throw new ForbiddenException("Cannot delete admin users");

        userRepository.Delete(user);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<CompanyResponseDto>> GetAllCompaniesAsync()
    {
        var companies = await companyRepository.GetAllAsync();
        return companies.Select(c => c.ToDto()).ToList();
    }

    public async Task VerifyCompanyAsync(Guid companyId)
    {
        var company = await companyRepository.GetByIdAsync(companyId)
            ?? throw new NotFoundException("Company", companyId);

        company.IsVerified = true;
        company.Tier = PlanTier.Standard;
        companyRepository.Update(company);
        await unitOfWork.SaveChangesAsync();

        await notificationService.CreateAsync(
            userId: company.UserId,
            title: NotificationMessages.CompanyVerifiedTitle,
            message: NotificationMessages.CompanyVerifiedMessage,
            type: NotificationType.CompanyVerified,
            actionUrl: $"/employer/company"
        );
    }

    public async Task RejectCompanyAsync(Guid companyId)
    {
        var company = await companyRepository.GetByIdAsync(companyId)
            ?? throw new NotFoundException("Company", companyId);

       
        company.IsVerified = false;
        company.Tier = PlanTier.Free;
        companyRepository.Update(company);
        await unitOfWork.SaveChangesAsync();

        await notificationService.CreateAsync(
            userId: company.UserId,
            title: NotificationMessages.CompanyRejectedTitle,
            message: NotificationMessages.CompanyRejectedMessage,
            type: NotificationType.CompanyRejected,
            actionUrl: $"/employer/company"
        );
    }
}