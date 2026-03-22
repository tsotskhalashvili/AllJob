using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Auth;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public bool IsPasswordChangeRequired { get; set; }


}
