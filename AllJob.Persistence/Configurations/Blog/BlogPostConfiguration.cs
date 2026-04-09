using AllJob.Domain.Entities.Blog;
using AllJob.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Blog;

public class BlogPostConfiguration : BaseEntityConfiguration<BlogPost>
{
    public override void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Author)
            .WithMany()
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.BlogCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasIndex(x => x.Slug)
            .IsUnique();

        builder.Property(x => x.Body)
            .IsRequired();

        builder.Property(x => x.CoverImageUrl)
            .HasMaxLength(512);
    }
}