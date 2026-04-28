using AllJob.Application.DTOs.Common;
using AllJob.Application.DTOs.Job;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Applications;
using AllJob.Application.Interfaces.Repositories.Companies;
using AllJob.Application.Interfaces.Repositories.Jobs;
using AllJob.Application.Interfaces.Repositories.Subscriptions;
using AllJob.Application.Interfaces.Services.Job;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Jobs;

namespace AllJob.Application.Services.Job;

public class JobService(
    IJobRepository jobRepository,
    ICompanyRepository companyRepository,
    IApplicationRepository applicationRepository,
    IPlanRepository planRepository,
    IUnitOfWork unitOfWork) : IJobService
{
    public async Task<JobResponseDto> CreateJobAsync(CreateJobDto dto, Guid userId)
    {
        var company = await companyRepository
            .GetByIdAsync(dto.CompanyId)
            ?? throw new NotFoundException("Company", dto.CompanyId);

        if (company.UserId != userId)
            throw new ForbiddenException();

        var activeJobs = await companyRepository
            .GetActiveJobsCountAsync(company.Id);

        var plan = await planRepository
            .GetByTierAsync(company.Tier)
            ?? throw new NotFoundException("Plan", company.Tier.ToString());

        if (activeJobs >= plan.MaxJobListings)
            throw new ForbiddenException(
                $"Job limit reached ({plan.MaxJobListings}). Please upgrade your plan.");

        var job = dto.ToEntity(dto.CompanyId);

        job.JobSkills = dto.SkillIds
            .Select(skillId => new JobSkill
            {
                JobId = job.Id,
                SkillId = skillId
            }).ToList();

        await jobRepository.AddAsync(job);
        await unitOfWork.SaveChangesAsync();

        var createdJob = await jobRepository
            .GetJobWithDetailsAsync(job.Id)
            ?? throw new NotFoundException("Job", job.Id);

        return createdJob.ToDto();
    }

    public async Task DeleteJobAsync(Guid id, Guid userId)
    {
        var job = await jobRepository.GetByIdAsync(id)
           ?? throw new NotFoundException("Job", id);

        var company = await companyRepository.GetByIdAsync(job.CompanyId)
            ?? throw new NotFoundException("Company", job.CompanyId);

        if (company.UserId != userId)
            throw new ForbiddenException();

        jobRepository.Delete(job);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<int> GetApplicationsCountAsync(Guid jobId, Guid userId)
    {
        var job = await jobRepository.GetByIdAsync(jobId)
            ?? throw new NotFoundException("Job", jobId);

        var company = await companyRepository.GetByIdAsync(job.CompanyId)
            ?? throw new NotFoundException("Company", job.CompanyId);

        if (company.UserId != userId)
            throw new ForbiddenException();

        return await applicationRepository.GetCountByJobIdAsync(jobId);
    }

    public async Task<JobResponseDto> GetJobByIdAsync(Guid id)
    {
        var job = await jobRepository.GetJobWithDetailsAsync(id)
              ?? throw new NotFoundException("Job", id);

        return job.ToDto();
    }

    public async Task<PagedResponseDto<JobResponseDto>> GetJobsAsync(JobFilterDto filter)
        => await jobRepository.GetPagedJobsAsync(filter);

    public async Task UpdateJobAsync(Guid id, UpdateJobDto dto, Guid userId)
    {
        var job = await jobRepository.GetJobWithDetailsAsync(id)
            ?? throw new NotFoundException("Job", id);


        if (job.Company.UserId != userId)
            throw new ForbiddenException();

        job.UpdateEntity(dto);

        if (dto.SkillIds is not null)
        {
            job.JobSkills.Clear();
            job.JobSkills = dto.SkillIds
                .Select(skillId => new JobSkill
                {
                    JobId = job.Id,
                    SkillId = skillId
                }).ToList();
        }

        jobRepository.Update(job);
        await unitOfWork.SaveChangesAsync();
    }
}