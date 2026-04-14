using AllJob.Application.DTOs.Admin;
using AllJob.Application.Interfaces.Repositories.Shared;
using AllJob.Application.Interfaces.Services.Admin;

namespace AllJob.Application.Services.Admin;

public class FullAccessService(
    IStatsRepository statsRepository) : IFullAccessService
{
    public async Task<AdminStatsDto> GetAdminStatsAsync()
    {
        var totalUsers = await statsRepository.GetTotalUsersCountAsync();
        var totalCompanies = await statsRepository.GetTotalCompaniesCountAsync();
        var totalJobs = await statsRepository.GetTotalJobsCountAsync();
        var activeJobs = await statsRepository.GetActiveJobsCountAsync();
        var totalApplications = await statsRepository.GetTotalApplicationsCountAsync();
        var newUsersToday = await statsRepository.GetNewUsersTodayCountAsync();
        var newJobsToday = await statsRepository.GetNewJobsTodayCountAsync();

        return new AdminStatsDto(
            TotalUsers: totalUsers,
            TotalCompanies: totalCompanies,
            TotalJobs: totalJobs,
            ActiveJobs: activeJobs,
            TotalApplications: totalApplications,
            NewUsersToday: newUsersToday,
            NewJobsToday: newJobsToday
        );
    }
}