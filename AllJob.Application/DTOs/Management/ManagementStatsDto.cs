namespace AllJob.Application.DTOs.Management;

public record ManagementStatsDto(
    int TotalUsers,
    int TotalCompanies,
    int TotalJobs,
    int ActiveJobs,
    int TotalApplications,
    int NewUsersToday,
    int NewJobsToday
);