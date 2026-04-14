using AllJob.Domain.Entities.Blog;

namespace AllJob.Application.Interfaces.Repositories.Blog
{
    public interface IBlogRepository : IGenericRepository<BlogPost>
    {
        Task<BlogPost?> GetBySlugAsync(string slug);
        Task<IReadOnlyList<BlogPost>> GetAllPublishedAsync();
    }
}
