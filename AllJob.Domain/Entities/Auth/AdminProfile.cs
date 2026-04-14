using AllJob.Domain.Common;
using AllJob.Domain.Entities.Auth;

namespace AllJob.Domain.Entities.Auth;

public class AdminProfile : BaseEntity
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
   
    public User User { get; set; } = null!;
}