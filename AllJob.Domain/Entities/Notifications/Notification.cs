using AllJob.Domain.Common;
using AllJob.Domain.Entities.Auth;
using AllJob.Domain.Enums.Notifications;

namespace AllJob.Domain.Entities.Notifications;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public NotificationType Type { get; set; } 
    public string Message { get; set; } = string.Empty;
    public string? ActionUrl { get; set; }
    public bool IsRead { get; set; }

    public User User { get; set; } = null!;
}