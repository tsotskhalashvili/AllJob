using AllJob.Domain.Entities.Candidate;
using AllJob.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Candidate;

public class CandidateExperienceConfiguration : BaseEntityConfiguration<CandidateExperience>
{
    public override void Configure(EntityTypeBuilder<CandidateExperience> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.CandidateProfile)
            .WithMany(x => x.Experiences)
            .HasForeignKey(x => x.CandidateProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.CompanyName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.Position)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired(false);
    }
}
