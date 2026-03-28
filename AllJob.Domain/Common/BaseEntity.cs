using AllJob.Domain.Interfaces;

namespace AllJob.Domain.Common;

public abstract class BaseEntity : IAuditable, ISoftDeletable
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
