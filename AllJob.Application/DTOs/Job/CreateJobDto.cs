using AllJob.Domain.Enums.Jobs;

namespace AllJob.Application.DTOs.Job;

public record CreateJobDto(
      Guid CompanyId,
    Guid CategoryId,
    Guid? AddressId,
    string Title,
    string Description,
    decimal? SalaryMin,
    decimal? SalaryMax,
    WorkType WorkType,
    ExperienceLevel? ExperienceLevel,
    DateTime ExpiresAt,
    List<Guid> SkillIds
);
