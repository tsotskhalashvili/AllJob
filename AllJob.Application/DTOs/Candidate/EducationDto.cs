namespace AllJob.Application.DTOs.Candidate;


public record EducationDto(
    Guid? Id,
    string InstitutionName,
    string Degree,
    string FieldOfStudy,
    DateTime StartDate,
    DateTime? EndDate
);
