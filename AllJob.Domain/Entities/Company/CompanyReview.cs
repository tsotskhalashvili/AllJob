using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Company;

public class CompanyReview : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
    public int Rating { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsAnonymous { get; set; }
}
