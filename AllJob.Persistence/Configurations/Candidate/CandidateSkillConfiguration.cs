using AllJob.Domain.Entities.Candidate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Candidate;

public class CandidateSkillConfiguration : IEntityTypeConfiguration<CandidateSkill>
{
    public void Configure(EntityTypeBuilder<CandidateSkill> builder)
    {
        builder.HasKey(x => new { x.CandidateProfileId, x.SkillId });

        builder.HasOne(x => x.CandidateProfile)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => x.CandidateProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.CandidateSkills)
            .HasForeignKey(x => x.SkillId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.CandidateProfile.IsDeleted && !x.Skill.IsDeleted);
    }
}
