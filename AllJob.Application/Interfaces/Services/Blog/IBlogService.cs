using AllJob.Application.DTOs.Blog;

namespace AllJob.Application.Interfaces.Services.Blog;

public interface IBlogService
{
    Task<IReadOnlyList<BlogPostResponseDto>> GetAllAsync();
    Task<BlogPostResponseDto> GetBySlugAsync(string slug);
    Task<IReadOnlyList<BlogCategoryResponseDto>> GetCategoriesAsync();
}