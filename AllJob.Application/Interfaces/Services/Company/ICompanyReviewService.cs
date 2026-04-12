using AllJob.Application.DTOs.Company;

namespace AllJob.Application.Interfaces.Services.Company;

public interface ICompanyReviewService
{
    Task<IReadOnlyList<CompanyReviewResponseDto>> GetCompanyReviewsAsync(Guid companyId);
    Task CreateReviewAsync(Guid companyId, CreateCompanyReviewDto dto, Guid userId);
}