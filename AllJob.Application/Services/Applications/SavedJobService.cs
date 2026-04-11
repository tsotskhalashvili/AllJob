using AllJob.Application.DTOs.Job;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Applications;
using AllJob.Application.Interfaces.Repositories.Jobs;
using AllJob.Application.Interfaces.Services.Applications;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Applications;

namespace AllJob.Application.Services.Applications;

public class SavedJobService(
    ISavedJobRepository savedJobRepository,
    IJobRepository jobRepository,
    IUnitOfWork unitOfWork) : ISavedJobService
{
    public async Task SaveJobAsync(Guid jobId, Guid userId)
    {
        _ = await jobRepository.GetByIdAsync(jobId)
            ?? throw new NotFoundException("Job", jobId);

        var existing = await savedJobRepository.GetAsync(userId, jobId);
        if (existing is not null)
            throw new ConflictException("Job is already saved.");

        var savedJob = new SavedJob
        {
            UserId = userId,
            JobId = jobId,
            SavedAt = DateTime.UtcNow
        };

        await savedJobRepository.AddAsync(savedJob);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UnsaveJobAsync(Guid jobId, Guid userId)
    {
        var savedJob = await savedJobRepository.GetAsync(userId, jobId)
            ?? throw new NotFoundException("SavedJob", jobId);

        savedJobRepository.Remove(savedJob);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<JobResponseDto>> GetMySavedJobsAsync(Guid userId)
    {
        var jobs = await savedJobRepository.GetSavedJobsAsync(userId);
        return jobs.Select(j => j.ToDto()).ToList();
    }
}