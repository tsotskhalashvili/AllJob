using AllJob.Application.DTOs.Candidate;

namespace AllJob.Application.Interfaces.Services.Candidate
{
    public interface ICandidateExperienceService
    {
        Task AddExperienceAsync(ExperienceDto dto, Guid userId);
        Task DeleteExperienceAsync(Guid experienceId, Guid userId);
        Task UpdateExperienceAsync(Guid experienceId, ExperienceDto dto, Guid userId);
    }
}
