using AllJob.Domain.Enums;

namespace AllJob.Application.DTOs.Job;

public record CreateJobDto(
    Guid CategoryId,
    Guid AddressId,
    string Title,
    string Description,
    decimal? SalaryMin,
    decimal? SalaryMax,
    WorkType WorkType,
    DateTime ExpiresAt,
    List<Guid> SkillIds
);