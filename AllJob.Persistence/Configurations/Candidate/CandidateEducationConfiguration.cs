using AllJob.Domain.Entities.Candidate;
using AllJob.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Candidate;

public class CandidateEducationConfiguration : BaseEntityConfiguration<CandidateEducation>
{
    public override void Configure(EntityTypeBuilder<CandidateEducation> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.CandidateProfile)
            .WithMany(x => x.Educations)
            .HasForeignKey(x => x.CandidateProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.InstitutionName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.Degree)
            .IsRequired();

        builder.Property(x => x.FieldOfStudy)
            .IsRequired()
            .HasMaxLength(256);
    }
}