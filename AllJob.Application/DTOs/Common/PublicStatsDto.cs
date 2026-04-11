    namespace AllJob.Application.DTOs.Common;

    public record PublicStatsDto(
        int TotalActiveJobs,
        int TotalCompanies,
        int TotalCandidates
    );