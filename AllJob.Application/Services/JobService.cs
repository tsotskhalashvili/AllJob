using AllJob.Application.DTOs.Common;
using AllJob.Application.DTOs.Job;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Services;
using AllJob.Application.Mappings;

namespace AllJob.Application.Services;

public class JobService(
    IJobRepository jobRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork
    ) : IJobService
{
    public async Task<JobResponseDto> CreateJobAsync(CreateJobDto dto)
    {
        var company = await companyRepository
       .GetByIdAsync(dto.CompanyId)
       ?? throw new NotFoundException("Company", dto.CompanyId);

        var job = dto.ToEntity(dto.CompanyId);

        await jobRepository.AddAsync(job);
        await unitOfWork.SaveChangesAsync();

        var createdJob = await jobRepository
            .GetJobWithDetailsAsync(job.Id)
            ?? throw new NotFoundException("Job", job.Id);

            return createdJob.ToDto();

        


    }

    public async Task DeleteJobAsync(Guid id, Guid companyId)
    {
        var job = await jobRepository.GetByIdAsync(id)
           ?? throw new NotFoundException("Job", id);

        if (job.CompanyId != companyId)
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

    public async Task UpdateJobAsync(Guid id, UpdateJobDto dto, Guid companyId)
    {
        var job = await jobRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Job", id);

        if (job.CompanyId != companyId)
            throw new ForbiddenException();

        job.UpdateEntity(dto);
        jobRepository.Update(job);
        await unitOfWork.SaveChangesAsync();
    }
}
