using AllJob.Application.DTOs.Common;
using AllJob.Application.DTOs.Job;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Services;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Jobs;
using AllJob.Domain.Enums.Subscriptions;

namespace AllJob.Application.Services;

public class JobService(
    IJobRepository jobRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork
    ) : IJobService
{
    public async Task<JobResponseDto> CreateJobAsync(CreateJobDto dto, Guid userId)
    {
        var company = await companyRepository
            .GetByIdAsync(dto.CompanyId)
            ?? throw new NotFoundException("Company", dto.CompanyId);

        if (company.UserId != userId)
            throw new ForbiddenException();

        // Plan limit შემოწმება:
        var activeJobs = await companyRepository
            .GetActiveJobsCountAsync(company.Id);

        var maxJobs = company.Tier switch
        {
            PlanTier.Free => 5,
            PlanTier.Standard => 15,
            PlanTier.VIP => 30,
            PlanTier.SuperVIP => int.MaxValue,
            _ => 5
        };

        if (activeJobs >= maxJobs)
            throw new ForbiddenException(
                $"You have reached your job limit ({maxJobs}). Please upgrade your plan.");

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

        var company = await companyRepository.GetByIdAsync(job.CompanyId)
            ?? throw new NotFoundException("Company", job.CompanyId);

        if (company.UserId != userId)
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