using AllJob.Application.Interfaces.Repositories.Blog;
using AllJob.Domain.Entities.Blog;
using AllJob.Persistence.Context;
using AllJob.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Blog;

public class BlogRepository(AppDbContext context)
    : GenericRepository<BlogPost>(context), IBlogRepository
{
    public async Task<BlogPost?> GetBySlugAsync(string slug)
        => await _dbSet
            .AsNoTracking()
            .Include(b => b.Category)
            .FirstOrDefaultAsync(b => b.Slug == slug);

    public async Task<IReadOnlyList<BlogPost>> GetAllPublishedAsync()
        => await _dbSet
            .AsNoTracking()
            .Include(b => b.Category)
            .Where(b => b.IsPublished)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
}