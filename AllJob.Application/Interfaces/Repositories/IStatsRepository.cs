namespace AllJob.Application.Interfaces.Repositories;

public interface IStatsRepository
{
    Task<int> GetActiveJobsCountAsync();
    Task<int> GetTotalCompaniesCountAsync();
    Task<int> GetTotalCandidatesCountAsync();
}
