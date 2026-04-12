namespace AllJob.Application.DTOs.Company;

public record CreateCompanyReviewDto(
    int Rating,
    string Title,
    string Body,
    bool IsAnonymous
);