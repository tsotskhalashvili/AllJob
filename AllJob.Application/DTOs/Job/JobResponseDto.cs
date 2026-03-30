using AllJob.Domain.Enums;

namespace AllJob.Application.DTOs.Job;

public record JobResponseDto(
    Guid Id,
    string CompanyName,
    string CategoryName,
    string Title,
    string Description,
    decimal? SalaryMin,
    decimal? SalaryMax,
    WorkType WorkType,
    JobStatus Status,
    DateTime ExpiresAt,
    DateTime CreatedAt,
    List<string> Skills
);