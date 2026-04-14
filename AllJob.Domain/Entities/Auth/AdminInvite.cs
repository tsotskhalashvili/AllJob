using AllJob.Domain.Common;
using AllJob.Domain.Enums.Auth;

namespace AllJob.Domain.Entities.Auth;

public class AdminInvite : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string TokenHash { get; set; } = string.Empty;
    public AdminRole Role { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedAt { get; set; }
    public Guid CreatedByUserId { get; set; }

    public User CreatedBy { get; set; } = null!;


}
