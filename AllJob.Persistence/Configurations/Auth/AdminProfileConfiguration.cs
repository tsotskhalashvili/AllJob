using AllJob.Domain.Entities.Auth;
using AllJob.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Auth;

public class AdminProfileConfiguration : BaseEntityConfiguration<AdminProfile>
{
    public override void Configure(EntityTypeBuilder<AdminProfile> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.User)
     .WithOne(u => u.AdminProfile)
     .HasForeignKey<AdminProfile>(x => x.UserId)
     .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.UserId)
            .IsUnique();
    }
}