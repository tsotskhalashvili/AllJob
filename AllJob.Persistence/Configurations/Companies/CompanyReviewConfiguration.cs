using AllJob.Domain.Entities.Companies;
using AllJob.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Companies;

public class CompanyReviewConfiguration : BaseEntityConfiguration<CompanyReview>
{
    public override void Configure(EntityTypeBuilder<CompanyReview> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.Body)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.Rating)
            .IsRequired();

        builder.HasCheckConstraint("CK_CompanyReviews_Rating", "[Rating] >= 1 AND [Rating] <= 5");
    }
}
