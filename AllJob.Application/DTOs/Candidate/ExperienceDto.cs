namespace AllJob.Application.DTOs.Candidate;


public record ExperienceDto(
    Guid? Id,
    string CompanyName,
    string Position,
    DateTime StartDate,
    DateTime? EndDate
);
