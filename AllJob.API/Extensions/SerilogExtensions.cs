using Serilog;

namespace AllJob.API.Extensions;

public static class SerilogExtensions
{
    public static IHostBuilder AddSerilogLogging(
        this IHostBuilder host)
    {
        host.UseSerilog((context, config) => config
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft",
                Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore",
                Serilog.Events.LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.Console()
            .WriteTo.File(
                path: "logs/alljob-.log",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30));

        return host;
    }
}