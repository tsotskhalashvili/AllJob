namespace AllJob.Application.DTOs.Candidate;


public record ExperienceDto(
    string CompanyName,
    string Position,
    DateTime StartDate,
    DateTime? EndDate
);
