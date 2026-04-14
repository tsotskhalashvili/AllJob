namespace AllJob.Application.Interfaces.Repositories.Shared;

public interface IStatsRepository
{
   
    Task<int> GetActiveJobsCountAsync();
    Task<int> GetTotalCompaniesCountAsync();
    Task<int> GetTotalCandidatesCountAsync();

    // Management
    Task<int> GetTotalUsersCountAsync();
    Task<int> GetTotalJobsCountAsync();
    Task<int> GetTotalApplicationsCountAsync();
    Task<int> GetNewUsersTodayCountAsync();
    Task<int> GetNewJobsTodayCountAsync();
}