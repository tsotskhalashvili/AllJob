namespace AllJob.Application.DTOs.Company;

public record CompanyReviewResponseDto(
    Guid Id,
    int Rating,
    string Title,
    string Body,
    bool IsAnonymous,
    string? AuthorName,
    DateTime CreatedAt
);