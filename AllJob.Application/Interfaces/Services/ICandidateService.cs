using AllJob.Application.DTOs.Candidate;

namespace AllJob.Application.Interfaces.Services;

public interface ICandidateService 
{
    Task<CandidateResponseDto> GetProfileAsync(Guid userId);
    Task<CandidateResponseDto> CreateProfileAsync(CreateCandidateProfileDto dto, Guid userId);
    Task<CandidateResponseDto> UpdateProfileAsync(UpdateCandidateProfileDto dto, Guid userId);
    Task DeleteProfileAsync(Guid userId);
}
