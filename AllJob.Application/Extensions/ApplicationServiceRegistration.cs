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

        return services;
    }
}
