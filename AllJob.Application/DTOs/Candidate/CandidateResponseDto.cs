namespace AllJob.Application.DTOs.Candidate;

public record CandidateResponseDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Bio,
    string? LinkedInUrl,
    string? PhotoUrl,
    string? Country,
    string? City,
    List<string> Skills,
    List<ExperienceDto> Experiences,
    List<EducationDto> Educations,
    DateTime CreatedAt
);
