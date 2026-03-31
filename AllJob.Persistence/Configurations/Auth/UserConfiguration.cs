using AllJob.Domain.Entities.Auth;
using AllJob.Persistence.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Auth;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .IsRequired();
    }
}