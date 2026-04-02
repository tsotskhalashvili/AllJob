namespace AllJob.Application.DTOs.Candidate;


public record UpdateCandidateProfileDto(
    string? FirstName,
    string? LastName,
    string? Bio,
    string? LinkedInUrl,
    string? PhotoUrl,
    Guid? AddressId
);
