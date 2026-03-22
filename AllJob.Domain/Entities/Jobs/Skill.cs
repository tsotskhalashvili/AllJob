using AllJob.Domain.Common;
using AllJob.Domain.Entities.Candidate;

namespace AllJob.Domain.Entities.Jobs;


public class Skill : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
    public ICollection<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();
}
