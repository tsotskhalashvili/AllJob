using AllJob.Application.DTOs.Common;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Services;

namespace AllJob.Application.Services;

public class StatsService(
    IStatsRepository statsRepository) : IStatsService
{
    public async Task<PublicStatsDto> GetPublicStatsAsync()
    {
        var activeJobs = await statsRepository.GetActiveJobsCountAsync();
        var companies = await statsRepository.GetTotalCompaniesCountAsync();
        var candidates = await statsRepository.GetTotalCandidatesCountAsync();

        return new PublicStatsDto(
            TotalActiveJobs: activeJobs,
            TotalCompanies: companies,
            TotalCandidates: candidates
        );
    }
}