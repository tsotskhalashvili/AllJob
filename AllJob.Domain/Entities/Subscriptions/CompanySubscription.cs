using AllJob.Domain.Common;
using AllJob.Domain.Entities.Companies;

namespace AllJob.Domain.Entities.Subscriptions;

public class CompanySubscription : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid PlanId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    public Company Company { get; set; } = null!;
    public Plan Plan { get; set; } = null!;
}