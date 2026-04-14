using AllJob.Application.DTOs.Blog;
using AllJob.Application.DTOs.Company;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Blog;
using AllJob.Application.Interfaces.Repositories.Companies;
using AllJob.Application.Interfaces.Services.Admin;
using AllJob.Application.Mappings;

namespace AllJob.Application.Services.Admin;

public class ContentModeratorService(
    ICompanyReviewRepository reviewRepository,
    IBlogRepository blogRepository,
    IUnitOfWork unitOfWork) : IContentModeratorService
{
    public async Task<IReadOnlyList<CompanyReviewResponseDto>> GetPendingReviewsAsync()
    {
        var reviews = await reviewRepository.GetPendingAsync();
        return reviews.Select(r => r.ToDto()).ToList();
    }

    public async Task ApproveReviewAsync(Guid reviewId)
    {
        var review = await reviewRepository.GetByIdAsync(reviewId)
            ?? throw new NotFoundException("Review", reviewId);

        review.IsApproved = true;
        reviewRepository.Update(review);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<BlogPostResponseDto> CreateBlogPostAsync(
        CreateBlogPostDto dto, Guid userId)
    {
        var post = dto.ToEntity(userId);
        await blogRepository.AddAsync(post);
        await unitOfWork.SaveChangesAsync();
        return post.ToDto();
    }

    public async Task UpdateBlogPostAsync(
        Guid id, CreateBlogPostDto dto, Guid userId)
    {
        var post = await blogRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("BlogPost", id);

        post.Title = dto.Title;
        post.Slug = dto.Slug;
        post.Body = dto.Body;
        post.CoverImageUrl = dto.CoverImageUrl;
        post.BlogCategoryId = dto.BlogCategoryId;

        blogRepository.Update(post);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteBlogPostAsync(Guid id)
    {
        var post = await blogRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("BlogPost", id);

        blogRepository.Delete(post);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task PublishBlogPostAsync(Guid id)
    {
        var post = await blogRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("BlogPost", id);

        post.IsPublished = true;
        blogRepository.Update(post);
        await unitOfWork.SaveChangesAsync();
    }
}