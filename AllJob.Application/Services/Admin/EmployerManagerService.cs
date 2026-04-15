using AllJob.Application.Constants;
using AllJob.Application.DTOs.Admin;
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
    public async Task<IReadOnlyList<UserResponseDto>> GetAllEmployersAsync()
    {
        var employers = await userRepository.GetAllEmployersAsync();
        return employers.Select(u => u.ToDto()).ToList();
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
        var user = await userRepository.GetByIdAsync(userId)
            ?? throw new NotFoundException("User", userId);

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