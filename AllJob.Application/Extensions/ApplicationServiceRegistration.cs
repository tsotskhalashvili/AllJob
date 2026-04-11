using AllJob.Application.Interfaces.Services;
using AllJob.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AllJob.Application.Extensions;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            Assembly.GetExecutingAssembly());

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICandidateService, CandidateService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<IJobCategoryService, JobCategoryService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IApplicationService, ApplicationService>();
        services.AddScoped<ISavedJobService, SavedJobService>();
        services.AddScoped<IPlanService, PlanService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IStatsService, StatsService>();


        return services;
    }
}