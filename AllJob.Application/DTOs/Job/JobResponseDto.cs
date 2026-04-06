using AllJob.Domain.Enums.Jobs;

namespace AllJob.Application.DTOs.Job;

public record JobResponseDto(
    Guid Id,
    string CompanyName,
    string CompanyLogoUrl,
    string CategoryName,
    string Title,
    string Description,
    decimal? SalaryMin,
    decimal? SalaryMax,
    string Country,
    string City,
    WorkType WorkType,
    JobStatus Status,
    DateTime ExpiresAt,
    DateTime CreatedAt,
    List<string> Skills
);
