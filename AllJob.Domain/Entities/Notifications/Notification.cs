using AllJob.Domain.Common;
using AllJob.Domain.Enums;

namespace AllJob.Domain.Entities.Notifications;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public NotificationType Type { get; set; } 
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
}