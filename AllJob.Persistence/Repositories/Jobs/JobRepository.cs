    using AllJob.Application.DTOs.Common;
    using AllJob.Application.DTOs.Job;
    using AllJob.Application.Interfaces.Repositories.Jobs;
    using AllJob.Application.Mappings;
    using AllJob.Domain.Entities.Jobs;
    using AllJob.Domain.Enums.Jobs;
    using AllJob.Persistence.Context;
    using AllJob.Persistence.Repositories.Common;
    using Microsoft.EntityFrameworkCore;

    namespace AllJob.Persistence.Repositories.Jobs;

    public class JobRepository(AppDbContext context)
        : GenericRepository<Job>(context), IJobRepository
    {
        public async Task<PagedResponseDto<JobResponseDto>> GetPagedJobsAsync(
            JobFilterDto filter)
        {
            var query = _dbSet
                .AsNoTracking()
                .Include(j => j.Company)
                .Include(j => j.Category)
                .Include(j => j.Address)
                .Include(j => j.JobSkills)
                    .ThenInclude(js => js.Skill)
                .Where(j => j.Status == JobStatus.Active)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(j => j.Title.StartsWith(filter.Title)); 

            if (filter.CategoryId.HasValue)
                query = query.Where(j => j.CategoryId == filter.CategoryId);

            if (filter.ExperienceLevel.HasValue)
                query = query.Where(j => j.ExperienceLevel == filter.ExperienceLevel);

            if (filter.SkillIds is not null && filter.SkillIds.Any())
                query = query.Where(j => j.JobSkills
                    .Any(js => filter.SkillIds.Contains(js.SkillId)));

            if (!string.IsNullOrEmpty(filter.Country))
                query = query.Where(j => j.Address!.Country == filter.Country);

            if (!string.IsNullOrEmpty(filter.City))
                query = query.Where(j => j.Address!.City == filter.City);

            if (filter.WorkType.HasValue)
                query = query.Where(j => j.WorkType == filter.WorkType);

            if (filter.SalaryMin.HasValue)
                query = query.Where(j => j.SalaryMin >= filter.SalaryMin);

            if (filter.SalaryMax.HasValue)
                query = query.Where(j => j.SalaryMax <= filter.SalaryMax);

            var totalCount = await query.CountAsync();

            var jobs = await query
                .OrderByDescending(j => j.Company.Tier)
                .ThenByDescending(j => j.CreatedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResponseDto<JobResponseDto>(
                Items: jobs.Select(j => j.ToDto()).ToList(),
                TotalCount: totalCount,
                Page: filter.Page,
                PageSize: filter.PageSize
            );
        }

        public async Task<Job?> GetJobWithDetailsAsync(Guid id)
            => await _dbSet
            
                .Include(j => j.Company)
                .Include(j => j.Category)
                .Include(j => j.Address)
                .Include(j => j.JobSkills)
                    .ThenInclude(js => js.Skill)
                .FirstOrDefaultAsync(j => j.Id == id);

        public async Task<IReadOnlyList<Job>> GetExpiredJobsAsync()
            => await _dbSet
          
                .Include(j => j.Company)
                .Where(j => j.ExpiresAt < DateTime.UtcNow
                    && j.Status != JobStatus.Expired)
                .ToListAsync();

        public async Task<IReadOnlyList<Job>> GetRecentJobsAsync(int hours)
        => await _dbSet
            .AsNoTracking()
            .Include(j => j.Company)
            .Include(j => j.Category)
            .Include(j => j.Address)
            .Include(j => j.JobSkills)
                .ThenInclude(js => js.Skill)
            .Where(j => j.Status == JobStatus.Active
                && j.CreatedAt > DateTime.UtcNow.AddHours(-hours))
            .ToListAsync();
    }