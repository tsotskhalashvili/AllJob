namespace AllJob.Application.DTOs.Candidate;


public record EducationDto(
    string InstitutionName,
    string Degree,
    string FieldOfStudy,
    DateTime StartDate,
    DateTime? EndDate
);
