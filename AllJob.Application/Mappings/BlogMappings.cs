using AllJob.Application.DTOs.Blog;
using AllJob.Domain.Entities.Blog;

namespace AllJob.Application.Mappings;

public static class BlogMappings
{
    public static BlogPostResponseDto ToDto(this BlogPost post)
        => new(
            Id: post.Id,
            Title: post.Title,
            Slug: post.Slug,
            Body: post.Body,
            CoverImageUrl: post.CoverImageUrl,
            AuthorName: post.Author.Email,
            CategoryName: post.Category.Name,
            IsPublished: post.IsPublished,
            ViewCount: post.ViewCount,
            CreatedAt: post.CreatedAt
        );

    public static BlogPost ToEntity(this CreateBlogPostDto dto, Guid authorId)
        => new()
        {
            Title = dto.Title,
            Slug = dto.Slug,
            Body = dto.Body,
            CoverImageUrl = dto.CoverImageUrl,
            AuthorId = authorId,
            BlogCategoryId = dto.BlogCategoryId,
            IsPublished = false,
            ViewCount = 0
        };

    public static BlogCategoryResponseDto ToDto(this BlogCategory category)
        => new(
            Id: category.Id,
            Name: category.Name,
            Slug: category.Slug
        );
}