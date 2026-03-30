namespace AllJob.Application.DTOs.Candidate;

public record CreateCandidateProfileDto(
    string FirstName,
    string LastName,
    string Bio,
    string LinkedInUrl,
    string PhotoUrl,
    Guid AddressId,
    List<Guid> SkillIds
);
