using AllJob.Domain.Common;
using AllJob.Domain.Entities.Auth;

namespace AllJob.Domain.Entities.Companies;

public class CompanyReview : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
    public int Rating { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsAnonymous { get; set; }
    public bool IsApproved { get; set; }

    public Company Company { get; set; } = null!;
    public User User { get; set; } = null!;
}
