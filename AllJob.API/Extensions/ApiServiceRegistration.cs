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
        services.AddSingleton<IExceptionResponseService, ExceptionResponseService>();

        services.AddScoped<IFileUploadService,
            CloudinaryService>();

        services.Configure<JwtSettings>(
    configuration.GetSection("JwtSettings"));

        services.Configure<CloudinarySettings>(
    configuration.GetSection("CloudinarySettings"));

        services.Configure<AdminSettings>(
    configuration.GetSection("AdminSettings"));

        return services;
    }
}
