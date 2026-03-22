using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Applications;

public class Application : BaseEntity
{
    public Guid JobId { get; set; }
    public Guid UserId { get; set; }
    public string? CoverLetter { get; set; }
    public string CvUrl { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime AppliedAt { get; set; }
}
