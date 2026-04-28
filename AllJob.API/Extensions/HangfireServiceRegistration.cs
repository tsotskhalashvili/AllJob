using AllJob.Application.Interfaces.Services.Hangfire;
using Hangfire;

namespace AllJob.API.Extensions;

public static class HangfireServiceRegistration
{
    public static IServiceCollection AddHangfireServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddHangfireServer();

        return services;
    }

    public static IApplicationBuilder UseHangfireServices(
        this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[] { new HangfireAuthFilter() }
        });

        RecurringJob.AddOrUpdate<IHangfireJobService>(
            "expire-jobs",
            x => x.ExpireJobsAsync(),
            Cron.Daily);

        RecurringJob.AddOrUpdate<IHangfireJobService>(
            "expire-subscriptions",
            x => x.ExpireSubscriptionsAsync(),
            Cron.Daily(1));

        RecurringJob.AddOrUpdate<IHangfireJobService>(
            "notify-new-jobs",
            x => x.NotifyNewJobsAsync(),
            Cron.HourInterval(6));

        RecurringJob.AddOrUpdate<IHangfireJobService>(
            "cleanup-tokens",
            x => x.CleanupExpiredTokensAsync(),
            Cron.Weekly(DayOfWeek.Sunday, 3));

        return app;
    }
}