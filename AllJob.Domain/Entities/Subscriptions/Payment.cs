using AllJob.Domain.Common;
using AllJob.Domain.Enums;

namespace AllJob.Domain.Entities.Subscriptions;

public class Payment : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid PlanId { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; } 
    public DateTime PaidAt { get; set; }
}