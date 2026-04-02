using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Settings;
using AllJob.Persistence.Context;
using AllJob.Persistence.Interceptors;
using AllJob.Persistence.Repositories;
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




        return services;
            

    }
}
