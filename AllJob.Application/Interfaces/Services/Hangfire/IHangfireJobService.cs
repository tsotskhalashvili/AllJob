namespace AllJob.Application.Interfaces.Services.Hangfire;

public interface IHangfireJobService
{
    Task ExpireJobsAsync();
    Task ExpireSubscriptionsAsync();
    Task NotifyNewJobsAsync();

    Task CleanupExpiredTokensAsync();

}
