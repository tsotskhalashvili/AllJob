using AllJob.Domain.Enums.Applications;

namespace AllJob.Application.DTOs.Application;

public record ApplicationResponseDto(
    Guid Id,
    Guid JobId,
    string JobTitle,
    string CompanyName,
    string? CvUrl,
    string? CoverLetter,
    ApplicationStatus Status
 
);
