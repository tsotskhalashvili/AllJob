using AllJob.Application.DTOs.Blog;
using AllJob.Application.DTOs.Company;

namespace AllJob.Application.Interfaces.Services.Admin;

public interface IContentModeratorService
{

    Task<IReadOnlyList<CompanyReviewResponseDto>> GetPendingReviewsAsync();
    Task ApproveReviewAsync(Guid reviewId);



   
    Task<BlogPostResponseDto> CreateBlogPostAsync(CreateBlogPostDto dto, Guid userId);
    Task UpdateBlogPostAsync(Guid id, CreateBlogPostDto dto, Guid userId);
    Task DeleteBlogPostAsync(Guid id);
}