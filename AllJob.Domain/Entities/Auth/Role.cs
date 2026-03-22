using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Auth;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

}
