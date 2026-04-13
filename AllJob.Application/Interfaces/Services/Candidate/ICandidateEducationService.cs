using AllJob.Application.DTOs.Candidate;

namespace AllJob.Application.Interfaces.Services.Candidate;

public interface ICandidateEducationService
{
    Task AddEducationAsync(EducationDto dto, Guid userId);
    Task DeleteEducationAsync(Guid educationId, Guid userId);
    Task UpdateEducationAsync(Guid educationId, EducationDto dto, Guid userId);

}
