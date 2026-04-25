using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Candidate;

public class CandidateExperience : BaseEntity
{
    public Guid CandidateProfileId { get; set; }
    public string CompanyName { get; set; } = string.Empty; 
    public string Position { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } 
    public DateTime? EndDate { get; set; }


    public CandidateProfile CandidateProfile { get; set; } = null!;
}
