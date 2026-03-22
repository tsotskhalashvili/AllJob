using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Jobs;

public class JobCategory : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;

    public ICollection<Job> Jobs { get; set; } = new List<Job>();
}
