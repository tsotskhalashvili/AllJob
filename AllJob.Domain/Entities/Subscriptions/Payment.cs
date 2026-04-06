using AllJob.Domain.Common;
using AllJob.Domain.Entities.Companies;
using AllJob.Domain.Enums.Subscriptions;

namespace AllJob.Domain.Entities.Subscriptions;

public class Payment : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid PlanId { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; } 
    public DateTime PaidAt { get; set; }

    public Company Company { get; set; } = null!;
    public Plan Plan { get; set; } = null!;
}