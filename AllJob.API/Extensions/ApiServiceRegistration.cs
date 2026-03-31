using AllJob.API.Services;
using AllJob.API.Services.Interfaces;
using AllJob.Application.Interfaces.Services;
using AllJob.Application.Settings;

namespace AllJob.API.Extensions;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiService(
        this IServiceCollection services,
         IConfiguration configuration)
    {
        services.AddScoped<IExceptionResponseService, ExceptionResponseService>();

        services.AddScoped<IFileUploadService,
            CloudinaryService>();

        services.Configure<CloudinarySettings>(
    configuration.GetSection("CloudinarySettings"));

        return services;
    }
}
