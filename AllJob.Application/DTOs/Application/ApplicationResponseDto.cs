using AllJob.Domain.Enums;

namespace AllJob.Application.DTOs.Application;

public record ApplicationResponseDto(
    Guid Id,
    Guid JobId,
    string JobTitle,
    string CompanyName,
    string CvUrl,
    string? CoverLetter,
    ApplicationStatus Status,
    DateTime AppliedAt
);