using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Candidate;

public class CandidateEducation : BaseEntity
{
    public Guid CandidateProfileId { get; set; }
    public string University { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string FieldOfStudy { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

}
