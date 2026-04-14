namespace AllJob.Application.DTOs.Admin;

public record AdminStatsDto(
    int TotalUsers,
    int TotalCompanies,
    int TotalJobs,
    int ActiveJobs,
    int TotalApplications,
    int NewUsersToday,
    int NewJobsToday
);