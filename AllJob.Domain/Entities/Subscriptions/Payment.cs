using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Subscriptions;

public class Payment : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid PlanId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime PaidAt { get; set; }
}