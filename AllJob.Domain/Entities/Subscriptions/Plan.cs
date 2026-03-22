using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Subscriptions;

public class Plan : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int MaxJobListings { get; set; }

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<CompanySubscription> Subscriptions { get; set; } = new List<CompanySubscription>();

}
