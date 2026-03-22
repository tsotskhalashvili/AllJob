using AllJob.Domain.Common;
using AllJob.Domain.Entities.Applications;
using AllJob.Domain.Entities.Companies;
using AllJob.Domain.Enums;

namespace AllJob.Domain.Entities.Jobs;

public class Job : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public bool IsRemote { get; set; }
    public JobStatus Status { get; set; } 
    public DateTime ExpiresAt { get; set; }

    public Company Company { get; set; } = null!;
    public JobCategory Category { get; set; } = null!;
    public ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
    public ICollection<Application> Applications { get; set; } = new List<Application>();
    public ICollection<SavedJob> SavedJobs { get; set; } = new List<SavedJob>();


}
