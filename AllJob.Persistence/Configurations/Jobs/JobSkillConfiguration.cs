using AllJob.Domain.Entities.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Jobs;

public class JobSkillConfiguration : IEntityTypeConfiguration<JobSkill>
{
    public void Configure(EntityTypeBuilder<JobSkill> builder)
    {
        builder.HasKey(x => new { x.JobId, x.SkillId });

        builder.HasOne(x => x.Job)
            .WithMany(x => x.JobSkills)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.JobSkills)
            .HasForeignKey(x => x.SkillId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(x => !x.Job.IsDeleted && !x.Skill.IsDeleted);
    }
}
