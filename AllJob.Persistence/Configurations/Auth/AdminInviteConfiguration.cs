using AllJob.Domain.Entities.Auth;
using AllJob.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Auth;

public class AdminInviteConfiguration : BaseEntityConfiguration<AdminInvite>
{
    public override void Configure(EntityTypeBuilder<AdminInvite> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.TokenHash)
        .IsRequired();

        builder.HasIndex(x => x.TokenHash)
            .IsUnique();

        builder.Property(x => x.Role)
            .IsRequired();
    }
}