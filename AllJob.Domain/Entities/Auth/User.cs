using AllJob.Domain.Common;
using AllJob.Domain.Entities.Candidate;
using AllJob.Domain.Entities.Companies;
using AllJob.Domain.Enums.Auth;

namespace AllJob.Domain.Entities.Auth;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public bool IsPasswordChangeRequired { get; set; }
    public bool IsExternalLogin { get; set; }
    public AdminRole? AdminRole { get; set; }

    // Navigation Properties
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public CandidateProfile? CandidateProfile { get; set; }
    public Company? Company { get; set; }
}