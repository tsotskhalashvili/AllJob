using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Repositories.Applications;
using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Application.Interfaces.Repositories.Blog;
using AllJob.Application.Interfaces.Repositories.Candidate;
using AllJob.Application.Interfaces.Repositories.Companies;
using AllJob.Application.Interfaces.Repositories.Jobs;
using AllJob.Application.Interfaces.Repositories.Messaging;
using AllJob.Application.Interfaces.Repositories.Notifications;
using AllJob.Application.Interfaces.Repositories.Shared;
using AllJob.Application.Interfaces.Repositories.Subscriptions;
using AllJob.Persistence.Context;
using AllJob.Persistence.Interceptors;
using AllJob.Persistence.Repositories.Applications;
using AllJob.Persistence.Repositories.Auth;
using AllJob.Persistence.Repositories.Blog;
using AllJob.Persistence.Repositories.Candidate;
using AllJob.Persistence.Repositories.Common;
using AllJob.Persistence.Repositories.Companies;
using AllJob.Persistence.Repositories.Jobs;
using AllJob.Persistence.Repositories.Messaging;
using AllJob.Persistence.Repositories.Notifications;
using AllJob.Persistence.Repositories.Shared;
using AllJob.Persistence.Repositories.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AllJob.Persistence.Extensions;

public static class PersistenceServiceRegistration
{

    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuditableEntityInterceptor>();
        services.AddScoped<SoftDeleteInterceptor>();


        services.AddDbContext<AppDbContext>((sp, options) =>
     options.UseSqlServer(
         configuration.GetConnectionString("DefaultConnection"))
         .AddInterceptors(
             sp.GetRequiredService<AuditableEntityInterceptor>(),
             sp.GetRequiredService<SoftDeleteInterceptor>()));



      
        services.AddScoped(typeof(IGenericRepository<>),
            typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<ISavedJobRepository, SavedJobRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();
        services.AddScoped<IStatsRepository, StatsRepository>();
        services.AddScoped<ICompanyReviewRepository, CompanyReviewRepository>();
        services.AddScoped<IAdminInviteRepository, AdminInviteRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();






        return services;
            

    }
}
