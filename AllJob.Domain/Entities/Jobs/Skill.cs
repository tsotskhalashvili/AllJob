using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Jobs;


public class Skill : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
