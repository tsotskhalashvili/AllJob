using AllJob.Domain.Enums.Applications;

namespace AllJob.Application.DTOs.Application;

public record ApplicationResponseDto(
    Guid Id,
    Guid JobId,
    Guid CandidateId,
    string JobTitle,
    string CompanyName,
    string? CvUrl,
    string? CoverLetter,
    ApplicationStatus Status,
    string? CandidateName,
    string? CandidateEmail,
    string? CandidatePhotoUrl,
    DateTime AppliedAt
);