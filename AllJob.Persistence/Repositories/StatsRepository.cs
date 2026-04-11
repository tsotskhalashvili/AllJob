using AllJob.Application.Interfaces.Repositories;
using AllJob.Domain.Enums.Jobs;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories;

public class StatsRepository(AppDbContext context) : IStatsRepository
{
    public async Task<int> GetActiveJobsCountAsync()
        => await context.Jobs
            .CountAsync(j => j.Status == JobStatus.Active);

    public async Task<int> GetTotalCompaniesCountAsync()
        => await context.Companies
            .CountAsync();

    public async Task<int> GetTotalCandidatesCountAsync()
        => await context.CandidateProfiles
            .CountAsync();
}