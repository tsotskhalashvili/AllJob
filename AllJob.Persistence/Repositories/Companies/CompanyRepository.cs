using AllJob.Application.DTOs.Common;
using AllJob.Application.DTOs.Company;
using AllJob.Application.Interfaces.Repositories.Companies;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Companies;
using AllJob.Domain.Enums.Jobs;
using AllJob.Persistence.Context;
using AllJob.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Companies;

public class CompanyRepository(AppDbContext context)
    : GenericRepository<Company>(context), ICompanyRepository
{
    public async Task<Company?> GetByUserIdAsync(Guid userId)
    => await _dbSet
        .Include(c => c.Payments)
        .FirstOrDefaultAsync(c => c.UserId == userId);
    public async Task<Company?> GetCompanyWithDetailsAsync(Guid id)
     => await _dbSet
         .AsNoTracking()
         .Include(c => c.Jobs)
             .ThenInclude(j => j.JobSkills)
                 .ThenInclude(js => js.Skill)
         .Include(c => c.Jobs)
             .ThenInclude(j => j.Category)
         .Include(c => c.Jobs)
             .ThenInclude(j => j.Address)
         .Include(c => c.Reviews)
         .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<int> GetActiveJobsCountAsync(Guid companyId)
        => await _dbSet
            .AsNoTracking()
            .Where(c => c.Id == companyId)
            .SelectMany(c => c.Jobs)
            .CountAsync(j => j.Status == JobStatus.Active);

    public async Task<PagedResponseDto<CompanyResponseDto>> GetPagedCompaniesAsync(
        CompanyFilterDto filter)
    {
        var query = _dbSet
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(c => c.Name.Contains(filter.Name));

        if (!string.IsNullOrEmpty(filter.Industry))
            query = query.Where(c => c.Industry == filter.Industry);

        if (filter.IsVerified.HasValue)
            query = query.Where(c => c.IsVerified == filter.IsVerified);

        var totalCount = await query.CountAsync();

        var companies = await query
            .Include(c => c.Reviews)
            .OrderByDescending(c => c.CreatedAt)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PagedResponseDto<CompanyResponseDto>(
            Items: companies.Select(c => c.ToDto()).ToList(),
            TotalCount: totalCount,
            Page: filter.Page,
            PageSize: filter.PageSize
        );
    }
}