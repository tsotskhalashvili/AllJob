using AllJob.Domain.Entities.Applications;
using AllJob.Domain.Entities.Auth;
using AllJob.Domain.Entities.Blog;
using AllJob.Domain.Entities.Candidate;
using AllJob.Domain.Entities.Companies;
using AllJob.Domain.Entities.Jobs;
using AllJob.Domain.Entities.Notifications;
using AllJob.Domain.Entities.Shared;
using AllJob.Domain.Entities.Subscriptions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AllJob.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Auth
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<AdminInvite> AdminInvites => Set<AdminInvite>();
    public DbSet<AdminProfile> AdminProfiles => Set<AdminProfile>();

    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();

    // Candidate
    public DbSet<CandidateProfile> CandidateProfiles => Set<CandidateProfile>();
    public DbSet<CandidateExperience> CandidateExperiences => Set<CandidateExperience>();
    public DbSet<CandidateEducation> CandidateEducations => Set<CandidateEducation>();
    public DbSet<CandidateSkill> CandidateSkills => Set<CandidateSkill>();

    // Company
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<CompanyReview> CompanyReviews => Set<CompanyReview>();

    // Jobs
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<JobCategory> JobCategories => Set<JobCategory>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<JobSkill> JobSkills => Set<JobSkill>();

    // Applications
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<SavedJob> SavedJobs => Set<SavedJob>();

    // Notifications
    public DbSet<Notification> Notifications => Set<Notification>();

    // Subscriptions
    public DbSet<Plan> Plans => Set<Plan>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<CompanySubscription> CompanySubscriptions => Set<CompanySubscription>();

    // Blog
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    public DbSet<BlogCategory> BlogCategories => Set<BlogCategory>();

    // Shared
    public DbSet<Address> Addresses => Set<Address>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly());
    }

  
} 
