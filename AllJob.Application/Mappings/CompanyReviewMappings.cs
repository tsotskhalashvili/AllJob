using AllJob.Application.DTOs.Company;
using AllJob.Domain.Entities.Companies;

namespace AllJob.Application.Mappings;

public static class CompanyReviewMappings
{
    public static CompanyReviewResponseDto ToDto(this CompanyReview review)
        => new(
            Id: review.Id,
            Rating: review.Rating,
            Title: review.Title,
            Body: review.Body,
            IsAnonymous: review.IsAnonymous,
            AuthorName: review.IsAnonymous ? null : review.User.Email,
            CreatedAt: review.CreatedAt
        );

    public static CompanyReview ToEntity(
        this CreateCompanyReviewDto dto, Guid companyId, Guid userId)
        => new()
        {
            CompanyId = companyId,
            UserId = userId,
            Rating = dto.Rating,
            Title = dto.Title,
            Body = dto.Body,
            IsAnonymous = dto.IsAnonymous,
            IsApproved = false
        };
}