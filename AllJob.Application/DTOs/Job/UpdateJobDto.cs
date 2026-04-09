using AllJob.Domain.Enums.Jobs;

namespace AllJob.Application.DTOs.Job;


public record UpdateJobDto(
   string? Title,
    string? Description,
    decimal? SalaryMin,
    decimal? SalaryMax,
    WorkType? WorkType,
    ExperienceLevel? ExperienceLevel,
    DateTime? ExpiresAt,
    List<Guid>? SkillIds
);
