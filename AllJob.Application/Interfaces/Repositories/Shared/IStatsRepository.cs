namespace AllJob.Application.Interfaces.Repositories.Shared;

public interface IStatsRepository
{
    Task<int> GetActiveJobsCountAsync();
    Task<int> GetTotalCompaniesCountAsync();
    Task<int> GetTotalCandidatesCountAsync();
}
