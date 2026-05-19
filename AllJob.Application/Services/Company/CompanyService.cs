using AllJob.Application.Constants;
using AllJob.Application.DTOs.Common;
using AllJob.Application.DTOs.Company;
using AllJob.Application.DTOs.Job;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Application.Interfaces.Repositories.Companies;
using AllJob.Application.Interfaces.Services.Company;
using AllJob.Application.Interfaces.Services.Notification;
using AllJob.Application.Mappings;
using AllJob.Domain.Enums.Auth;
using AllJob.Domain.Enums.Jobs;
using AllJob.Domain.Enums.Notifications;

namespace AllJob.Application.Services.Company;

public class CompanyService(
    ICompanyRepository companyRepository,
    IUserRepository userRepository,
    INotificationService notificationService,
    IUnitOfWork unitOfWork) : ICompanyService
{
    public async Task<CompanyResponseDto> GetCompanyByIdAsync(Guid id)
    {
        var company = await companyRepository
            .GetCompanyWithDetailsAsync(id)
            ?? throw new NotFoundException("Company", id);

        return company.ToDto();
    }

    public async Task<CompanyResponseDto> CreateCompanyAsync(
      CreateCompanyDto dto, Guid userId)
    {
        var existing = await companyRepository.GetByUserIdAsync(userId);
        if (existing is not null)
            throw new ConflictException("You already have a registered company");

        var company = dto.ToEntity(userId);
        await companyRepository.AddAsync(company);
        await unitOfWork.SaveChangesAsync();

        var admins = await userRepository.GetAllAdminsAsync(1, int.MaxValue);
        foreach (var admin in admins.Items.Where(a =>
            a.AdminRole == AdminRole.EmployerManager ||
            a.AdminRole == AdminRole.FullAccess))
        {
            await notificationService.CreateAsync(
                userId: admin.Id,
                title: NotificationMessages.NewCompanyPendingTitle,
                message: NotificationMessages.NewCompanyPendingMessage,
                type: NotificationType.NewCompanyPending,
                actionUrl: $"/admin/companies"
            );
        }

        return company.ToDto();
    }
    public async Task UpdateCompanyAsync(
        Guid id, UpdateCompanyDto dto, Guid userId)
    {
        var company = await companyRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Company", id);

        if (company.UserId != userId)
            throw new ForbiddenException();

        company.UpdateEntity(dto);
        companyRepository.Update(company);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCompanyAsync(Guid id, Guid userId)
    {
        var company = await companyRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Company", id);

        if (company.UserId != userId)
            throw new ForbiddenException();

        companyRepository.Delete(company);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<PagedResponseDto<CompanyResponseDto>> GetCompaniesAsync(
        CompanyFilterDto filter)
        => await companyRepository.GetPagedCompaniesAsync(filter);

    public async Task<IReadOnlyList<JobResponseDto>> GetCompanyJobsAsync(
    Guid companyId, Guid? requestingUserId = null)
    {
        var company = await companyRepository
            .GetCompanyWithDetailsAsync(companyId)
            ?? throw new NotFoundException("Company", companyId);

       
        if (requestingUserId.HasValue && company.UserId == requestingUserId)
            return company.Jobs.Select(j => j.ToDto()).ToList();

        
        return company.Jobs
            .Where(j => j.Status == JobStatus.Active)
            .Select(j => j.ToDto())
            .ToList();
    }

    public async Task<CompanyResponseDto?> GetMyCompanyAsync(Guid userId)
    {
        var company = await companyRepository.GetByEmployerIdAsync(userId);
        return company?.ToDto();
    }
}