using AllJob.Domain.Entities.Jobs;
using AllJob.Domain.Entities.Candidate;
using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Shared;

public class Address : BaseEntity
{
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    // Navigation Properties
    public ICollection<Job> Jobs { get; set; } = new List<Job>();
    public ICollection<CandidateProfile> CandidateProfiles { get; set; } = new List<CandidateProfile>();
}