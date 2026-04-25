using AllJob.Domain.Common;
using AllJob.Domain.Enums.Candidate;

namespace AllJob.Domain.Entities.Candidate;

public class CandidateEducation : BaseEntity
{
    public Guid CandidateProfileId { get; set; }
    public string InstitutionName { get; set; } = string.Empty; 
    public DegreeType Degree { get; set; }   
    public string FieldOfStudy { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public CandidateProfile CandidateProfile { get; set; } = null!;


}
