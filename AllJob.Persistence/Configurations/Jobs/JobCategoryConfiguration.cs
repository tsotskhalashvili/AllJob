using AllJob.Domain.Entities.Jobs;
using AllJob.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Jobs;

public class JobCategoryConfiguration : BaseEntityConfiguration<JobCategory>
{
    public override void Configure(EntityTypeBuilder<JobCategory> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.Slug)
            .IsUnique();

        builder.Property(x => x.IconUrl)
            .HasMaxLength(512);
    }
}
