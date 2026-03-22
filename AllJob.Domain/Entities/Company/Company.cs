using AllJob.Domain.Common;

namespace AllJob.Domain.Entities.Company;

public class Company : BaseEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
}
