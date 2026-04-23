using AllJob.Domain.Common;
using AllJob.Domain.Entities.Auth;

namespace AllJob.Domain.Entities.Messaging;

public class Conversation : BaseEntity
{
    public Guid CandidateId { get; set; }
    public Guid EmployerId { get; set; }
    public DateTime LastMessageAt { get; set; }

    public User Candidate { get; set; } = null!;
    public User Employer { get; set; } = null!;
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}