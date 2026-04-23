using AllJob.Domain.Entities.Messaging;
using AllJob.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Messaging;

public class ConversationConfiguration : BaseEntityConfiguration<Conversation>
{
    public override void Configure(EntityTypeBuilder<Conversation> builder)
    {
        base.Configure(builder);

        builder.HasOne(c => c.Candidate)
            .WithMany()
            .HasForeignKey(c => c.CandidateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Employer)
            .WithMany()
            .HasForeignKey(c => c.EmployerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Messages)
            .WithOne(m => m.Conversation)
            .HasForeignKey(m => m.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}