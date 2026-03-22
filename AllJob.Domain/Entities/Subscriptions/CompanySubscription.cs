using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Subscriptions;

public class CompanySubscription : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid PlanId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}