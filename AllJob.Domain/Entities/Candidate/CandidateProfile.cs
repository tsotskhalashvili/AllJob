using AllJob.Domain.Common;
using AllJob.Domain.Entities.Auth;
using AllJob.Domain.Entities.Shared;

namespace AllJob.Domain.Entities.Candidate;

public class CandidateProfile : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid? AddressId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;

    public string? LinkedInUrl { get; set; } 
    public string? PhotoUrl { get; set; } 
    public bool IsOpenToWork { get; set; }


   // Navigation Properties
    public User User { get; set; } = null!;
    public Address? Address { get; set; } 
    public ICollection<CandidateExperience> Experiences { get; set; } = new List<CandidateExperience>();
    public ICollection<CandidateEducation> Educations { get; set; } = new List<CandidateEducation>();
    public ICollection<CandidateSkill> Skills { get; set; } = new List<CandidateSkill>();

}
