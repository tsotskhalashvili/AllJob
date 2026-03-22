using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Candidate;

public class CandidateProfile : BaseEntity
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    public string LinkedInUrl { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;

}
