using AllJob.Domain.Entities.Companies;
using AllJob.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllJob.Persistence.Configurations.Companies;

public class CompanyConfiguration : BaseEntityConfiguration<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.User)
            .WithOne(x => x.Company)
            .HasForeignKey<Company>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Jobs)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Payments)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Subscriptions)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.LogoUrl)
            .HasMaxLength(512);

        builder.Property(x => x.Website)
            .HasMaxLength(256);

        builder.Property(x => x.Industry)
            .HasMaxLength(100);

        builder.Property(x => x.FacebookUrl)
            .HasMaxLength(256);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);
    }
}
