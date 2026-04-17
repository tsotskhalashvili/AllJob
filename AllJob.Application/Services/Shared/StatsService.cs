using AllJob.Application.DTOs.Common;
using AllJob.Application.Interfaces.Repositories.Shared;
using AllJob.Application.Interfaces.Services.Shared;

namespace AllJob.Application.Services.Shared;

public class StatsService(
    IStatsRepository statsRepository,
    ICacheService cacheService) : IStatsService
{
    private const string CacheKey = "stats:public";

    public async Task<PublicStatsDto> GetPublicStatsAsync()
    {
        var cached = cacheService.Get<PublicStatsDto>(CacheKey);
        if (cached is not null) return cached;

        var activeJobs = await statsRepository.GetActiveJobsCountAsync();
        var companies = await statsRepository.GetTotalCompaniesCountAsync();
        var candidates = await statsRepository.GetTotalCandidatesCountAsync();

        var result = new PublicStatsDto(
            TotalActiveJobs: activeJobs,
            TotalCompanies: companies,
            TotalCandidates: candidates
        );

        cacheService.Set(CacheKey, result, TimeSpan.FromMinutes(15));
        return result;
    }
}