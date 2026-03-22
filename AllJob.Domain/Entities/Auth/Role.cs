using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Auth;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
