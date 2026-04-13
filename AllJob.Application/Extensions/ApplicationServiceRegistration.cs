using AllJob.Application.Interfaces.Services.Admin;
using AllJob.Application.Interfaces.Services.Applications;
using AllJob.Application.Interfaces.Services.Auth;
using AllJob.Application.Interfaces.Services.Candidate;
using AllJob.Application.Interfaces.Services.Company;
using AllJob.Application.Interfaces.Services.Job;
using AllJob.Application.Interfaces.Services.Notification;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Interfaces.Services.Subscription;
using AllJob.Application.Services.Admin;
using AllJob.Application.Services.Applications;
using AllJob.Application.Services.Auth;
using AllJob.Application.Services.Candidate;
using AllJob.Application.Services.Company;
using AllJob.Application.Services.Job;
using AllJob.Application.Services.Notification;
using AllJob.Application.Services.Shared;
using AllJob.Application.Services.Subscription;
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
        services.AddScoped<ICompanyReviewService, CompanyReviewService>();
        services.AddScoped<ICandidateExperienceService, CandidateExperienceService>();
        services.AddScoped<ICandidateEducationService, CandidateEducationService>();


        return services;
    }
}