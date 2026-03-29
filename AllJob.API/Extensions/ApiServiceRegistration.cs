using AllJob.API.Services;
using AllJob.API.Services.Interfaces;

namespace AllJob.API.Extensions;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiService(
        this IServiceCollection services)
    {
        services.AddScoped<IExceptionResponseService, ExceptionResponseService>();

        return services;
    }
}
