namespace AllJob.Application.DTOs.Application;

public record CreateApplicationDto(
    Guid JobId,
    string CvUrl,
    string? CoverLetter
);
