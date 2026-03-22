using AllJob.Domain.Entities.Auth;
using AllJob.Domain.Entities.Jobs;

namespace AllJob.Domain.Entities.Applications;

public class SavedJob
{
    public Guid UserId { get; set; }
    public Guid JobId { get; set; }
    public DateTime SavedAt { get; set; }

    public User User { get; set; } = null!;
    public Job Job { get; set; } = null!;
}