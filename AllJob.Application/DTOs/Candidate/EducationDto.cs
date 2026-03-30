namespace AllJob.Application.DTOs.Candidate;


public record EducationDto(
    string University,
    string Degree,
    string FieldOfStudy,
    DateTime StartDate,
    DateTime? EndDate
);
