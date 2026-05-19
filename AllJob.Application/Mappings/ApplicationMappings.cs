using AllJob.Application.DTOs.Application;
using AllJob.Domain.Entities.Applications;
using AllJob.Domain.Enums.Applications;


namespace AllJob.Application.Mappings;

public static class ApplicationMappings
{
    public static ApplicationResponseDto ToDto(this JobApplication application)
         => new(
             Id: application.Id,
             JobId: application.JobId,
             CandidateId: application.User?.CandidateProfile?.Id ?? application.UserId,
             JobTitle: application.Job.Title,
             CompanyName: application.Job.Company.Name,
             CvUrl: application.CvUrl,
             CoverLetter: application.CoverLetter,
             Status: application.Status,
             CandidateName: application.User?.CandidateProfile != null
                 ? $"{application.User.CandidateProfile.FirstName} {application.User.CandidateProfile.LastName}".Trim()
                 : application.User?.Email,
             CandidateEmail: application.User?.Email,
             CandidatePhotoUrl: application.User?.CandidateProfile?.PhotoUrl,
             AppliedAt: application.CreatedAt
         );
    public static JobApplication ToEntity(
        this CreateApplicationDto dto, Guid userId)
        => new()
        {
            JobId = dto.JobId,
            UserId = userId,
            CvUrl = dto.CvUrl,
            CoverLetter = dto.CoverLetter,
            Status = ApplicationStatus.Pending
        };
}
