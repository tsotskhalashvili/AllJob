using AllJob.Application.DTOs.Application;
using AllJob.Domain.Entities.Applications;

namespace AllJob.Application.Mappings;

public static class ApplicationMappings
{
    public static ApplicationResponseDto ToDto(this JobApplication application)
        => new(
            Id: application.Id,
            JobId: application.JobId,
            JobTitle: application.Job.Title,
            CompanyName: application.Job.Company.Name,
            CvUrl: application.CvUrl,
            CoverLetter: application.CoverLetter,
            Status: application.Status,
            AppliedAt: application.AppliedAt
        );

    public static JobApplication ToEntity(
        this CreateApplicationDto dto, Guid userId)
        => new()
        {
            JobId = dto.JobId,
            UserId = userId,
            CvUrl = dto.CvUrl,
            CoverLetter = dto.CoverLetter,
            AppliedAt = DateTime.UtcNow,
            Status = Domain.Enums.ApplicationStatus.Pending
        };
}
