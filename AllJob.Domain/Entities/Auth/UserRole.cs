using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Auth;

public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
