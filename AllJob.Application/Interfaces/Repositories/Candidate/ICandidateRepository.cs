using AllJob.Domain.Entities.Candidate;

namespace AllJob.Application.Interfaces.Repositories.Candidate;

public interface ICandidateRepository : IGenericRepository<CandidateProfile>
{
    
    Task<CandidateProfile?> GetCandidateWithDetailsAsync(Guid userId);
}