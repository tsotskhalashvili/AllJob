using AllJob.Application.Interfaces.Repositories.Shared;
using AllJob.Domain.Enums.Jobs;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Shared;

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

    public async Task<int> GetTotalUsersCountAsync()
    => await context.Users.CountAsync();

    public async Task<int> GetTotalJobsCountAsync()
        => await context.Jobs.CountAsync();

    public async Task<int> GetTotalApplicationsCountAsync()
        => await context.JobApplications.CountAsync();

    public async Task<int> GetNewUsersTodayCountAsync()
        => await context.Users
            .CountAsync(u => u.CreatedAt.Date == DateTime.UtcNow.Date);

    public async Task<int> GetNewJobsTodayCountAsync()
        => await context.Jobs
            .CountAsync(j => j.CreatedAt.Date == DateTime.UtcNow.Date);
}