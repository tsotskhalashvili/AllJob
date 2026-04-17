using Hangfire.Dashboard;

public class HangfireAuthFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

     
        if (httpContext.RequestServices
            .GetRequiredService<IWebHostEnvironment>()
            .IsDevelopment())
            return true;

        return httpContext.User.IsInRole("SuperAdmin");
    }
}