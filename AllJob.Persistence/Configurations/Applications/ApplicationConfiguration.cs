using AllJob.Domain.Entities.Applications;
using AllJob.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Applications;

public class ApplicationConfiguration : BaseEntityConfiguration<Application>
{
    public override void Configure(EntityTypeBuilder<Application> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Job)
            .WithMany(x => x.Applications)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.CoverLetter)
            .HasMaxLength(3000);

        builder.Property(x => x.CvUrl)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();
    }
}
