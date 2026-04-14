using AllJob.Application.DTOs.Blog;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces.Repositories.Blog;
using AllJob.Application.Interfaces.Services.Blog;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Blog;
using AllJob.Application.Interfaces.Repositories;

namespace AllJob.Application.Services.Blog;

public class BlogService(
    IBlogRepository blogRepository,
    IGenericRepository<BlogCategory> categoryRepository) : IBlogService
{
    public async Task<IReadOnlyList<BlogPostResponseDto>> GetAllAsync()
    {
        var posts = await blogRepository.GetAllPublishedAsync();
        return posts.Select(p => p.ToDto()).ToList();
    }

    public async Task<BlogPostResponseDto> GetBySlugAsync(string slug)
    {
        var post = await blogRepository.GetBySlugAsync(slug)
            ?? throw new NotFoundException("BlogPost", slug);

        return post.ToDto();
    }

    public async Task<IReadOnlyList<BlogCategoryResponseDto>> GetCategoriesAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return categories.Select(c => c.ToDto()).ToList();
    }
}