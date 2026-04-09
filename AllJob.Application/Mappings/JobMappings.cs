using AllJob.Application.DTOs.Job;
using AllJob.Domain.Entities.Jobs;
using AllJob.Domain.Enums.Jobs;

namespace AllJob.Application.Mappings;

public static class JobMappings
{
    public static JobResponseDto ToDto(this Job job)
        => new(
            Id: job.Id,
            CompanyName: job.Company.Name,
            CompanyLogoUrl: job.Company.LogoUrl,
            CategoryName: job.Category.Name,
            Title: job.Title,
            Description: job.Description,
            SalaryMin: job.SalaryMin,
            SalaryMax: job.SalaryMax,
            Country: job.Address?.Country,
            City: job.Address?.City,
            WorkType: job.WorkType,
            Status: job.Status,
            ExperienceLevel: job.ExperienceLevel,
            ExpiresAt: job.ExpiresAt,
            CreatedAt: job.CreatedAt,
            Skills: job.JobSkills
                .Select(js => js.Skill.Name)
                .ToList()
        );

    public static Job ToEntity(this CreateJobDto dto, Guid companyId)
        => new()
        {
            CompanyId = companyId,
            CategoryId = dto.CategoryId,
            AddressId = dto.AddressId,
            Title = dto.Title,
            Description = dto.Description,
            SalaryMin = dto.SalaryMin,
            SalaryMax = dto.SalaryMax,
            WorkType = dto.WorkType,
            ExperienceLevel = dto.ExperienceLevel,
            Status = JobStatus.Draft,
            ExpiresAt = dto.ExpiresAt
        };

    public static void UpdateEntity(this Job job, UpdateJobDto dto)
    {
        if (dto.Title is not null) job.Title = dto.Title;
        if (dto.Description is not null) job.Description = dto.Description;
        if (dto.SalaryMin is not null) job.SalaryMin = dto.SalaryMin;
        if (dto.SalaryMax is not null) job.SalaryMax = dto.SalaryMax;
        if (dto.WorkType is not null) job.WorkType = dto.WorkType.Value;
        if (dto.ExperienceLevel is not null) job.ExperienceLevel = dto.ExperienceLevel;
        if (dto.ExpiresAt is not null) job.ExpiresAt = dto.ExpiresAt.Value;
    }
}