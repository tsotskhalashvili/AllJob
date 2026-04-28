using Hangfire.Dashboard;

namespace AllJob.API.Extensions;

public class HangfireAuthFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

        var env = httpContext.RequestServices
     .GetRequiredService<IWebHostEnvironment>();

        if (env.IsDevelopment())
            return true;

        if (!httpContext.User.Identity?.IsAuthenticated ?? true)
            return false;

        return httpContext.User.IsInRole("SuperAdmin");

     
    }
}