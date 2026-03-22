using AllJob.Domain.Entities.Jobs;

namespace AllJob.Domain.Entities.Candidate;

public class CandidateSkill
{

    public Guid CandidateProfileId { get; set; }
    public Guid SkillId { get; set; }

    public CandidateProfile CandidateProfile { get; set; } = null!;
    public Skill Skill { get; set; } = null!;



}
