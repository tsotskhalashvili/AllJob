using AllJob.Domain.Common;
using AllJob.Domain.Entities.Candidate;
using AllJob.Domain.Entities.Companies;

namespace AllJob.Domain.Entities.Auth;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public bool IsPasswordChangeRequired { get; set; }

    // Navigation Properties
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public CandidateProfile? CandidateProfile { get; set; }
    public Company? Company { get; set; }
}