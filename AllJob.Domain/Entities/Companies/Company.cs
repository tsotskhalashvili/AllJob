using AllJob.Domain.Common;
using AllJob.Domain.Entities.Auth;
using AllJob.Domain.Entities.Jobs;
using AllJob.Domain.Entities.Subscriptions;

namespace AllJob.Domain.Entities.Companies;

public class Company : BaseEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public bool IsVerified { get; set; }

    public User User { get; set; } = null!;
    public ICollection<Job> Jobs { get; set; } = new List<Job>();
    public ICollection<CompanyReview> Reviews { get; set; } = new List<CompanyReview>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<CompanySubscription> Subscriptions { get; set; } = new List<CompanySubscription>();
}
