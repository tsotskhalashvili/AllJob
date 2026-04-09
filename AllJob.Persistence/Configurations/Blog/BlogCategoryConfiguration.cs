using AllJob.Domain.Entities.Blog;
using AllJob.Persistence.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Blog;

public class BlogCategoryConfiguration : BaseEntityConfiguration<BlogCategory>
{
    public override void Configure(EntityTypeBuilder<BlogCategory> builder)
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
    }
}