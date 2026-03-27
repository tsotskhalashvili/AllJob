using AllJob.Domain.Common;
using AllJob.Domain.Entities.Auth;
using AllJob.Domain.Entities.Jobs;
using AllJob.Domain.Enums;

namespace AllJob.Domain.Entities.Applications;

public class JobApplication : BaseEntity
{
    public Guid JobId { get; set; }
    public Guid UserId { get; set; }
    public string? CoverLetter { get; set; }
    public string CvUrl { get; set; } = string.Empty;
    public ApplicationStatus Status { get; set; } 
    public DateTime AppliedAt { get; set; }

    public Job Job { get; set; } = null!;
    public User User { get; set; } = null!;
}
