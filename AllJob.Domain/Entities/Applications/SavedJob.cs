namespace AllJob.Domain.Entities.Applications;

public class SavedJob
{
    public Guid UserId { get; set; }
    public Guid JobId { get; set; }
    public DateTime SavedAt { get; set; }
}