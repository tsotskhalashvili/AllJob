using AllJob.Application.DTOs.Application;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Services;
using AllJob.Application.Mappings;

namespace AllJob.Application.Services;

public class ApplicationService(
    IApplicationRepository applicationRepository,
    IJobRepository jobRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork) : IApplicationService
{
    public async Task<ApplicationResponseDto> CreateAsync(
        CreateApplicationDto dto, Guid userId)
    {
        var job = await jobRepository.GetByIdAsync(dto.JobId)
            ?? throw new NotFoundException("Job", dto.JobId);

        var existing = await applicationRepository
            .GetCandidateApplicationsAsync(userId);

        if (existing.Any(a => a.JobId == dto.JobId))
            throw new ConflictException("You have already applied for this job.");

        var application = dto.ToEntity(userId);
        await applicationRepository.AddAsync(application);
        await unitOfWork.SaveChangesAsync();

        var applications = await applicationRepository
            .GetCandidateApplicationsAsync(userId);

        return applications.First(a => a.Id == application.Id).ToDto();
    }

    public async Task<IReadOnlyList<ApplicationResponseDto>> GetMyApplicationsAsync(
        Guid userId)
    {
        var applications = await applicationRepository
            .GetCandidateApplicationsAsync(userId);

        return applications.Select(a => a.ToDto()).ToList();
    }

    public async Task<IReadOnlyList<ApplicationResponseDto>> GetJobApplicationsAsync(
        Guid jobId, Guid userId)
    {
        var job = await jobRepository.GetByIdAsync(jobId)
            ?? throw new NotFoundException("Job", jobId);

        var company = await companyRepository.GetByIdAsync(job.CompanyId)
            ?? throw new NotFoundException("Company", job.CompanyId);

        if (company.UserId != userId)
            throw new ForbiddenException();

        var applications = await applicationRepository
            .GetJobApplicationsAsync(jobId);

        return applications.Select(a => a.ToDto()).ToList();
    }

    public async Task<ApplicationResponseDto> UpdateStatusAsync(
        Guid applicationId, UpdateApplicationStatusDto dto, Guid userId)
    {
        var application = await applicationRepository.GetByIdAsync(applicationId)
            ?? throw new NotFoundException("Application", applicationId);

        var job = await jobRepository.GetByIdAsync(application.JobId)
            ?? throw new NotFoundException("Job", application.JobId);

        var company = await companyRepository.GetByIdAsync(job.CompanyId)
            ?? throw new NotFoundException("Company", job.CompanyId);

        if (company.UserId != userId)
            throw new ForbiddenException();

        application.Status = dto.Status;
        applicationRepository.Update(application);
        await unitOfWork.SaveChangesAsync();

        var applications = await applicationRepository
            .GetJobApplicationsAsync(application.JobId);

        return applications.First(a => a.Id == applicationId).ToDto();
    }
}