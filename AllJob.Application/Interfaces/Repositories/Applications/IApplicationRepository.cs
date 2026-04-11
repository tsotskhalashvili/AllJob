using AllJob.Domain.Entities.Applications;

namespace AllJob.Application.Interfaces.Repositories.Applications;

public interface IApplicationRepository : IGenericRepository<JobApplication>
{
    Task<IReadOnlyList<JobApplication>> GetCandidateApplicationsAsync(Guid userId);

    Task<IReadOnlyList<JobApplication>> GetJobApplicationsAsync(Guid jobId);

}
