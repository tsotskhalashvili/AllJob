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
            Country: job.Address.Country,
            City: job.Address.City,
            WorkType: job.WorkType,
            Status: job.Status,
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
          Status = JobStatus.Draft,
          ExpiresAt = dto.ExpiresAt
      };

    public static void UpdateEntity(this Job job, UpdateJobDto dto)
    {
        job.Title = dto.Title;
        job.Description = dto.Description;
        job.SalaryMin = dto.SalaryMin;
        job.SalaryMax = dto.SalaryMax;
        job.WorkType = dto.WorkType;
        job.ExpiresAt = dto.ExpiresAt;
    }

}
