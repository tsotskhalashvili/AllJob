using AllJob.Application.DTOs.Company;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Companies;
using AllJob.Application.Interfaces.Services.Company;
using AllJob.Application.Mappings;

namespace AllJob.Application.Services.Company;

public class CompanyReviewService(
    ICompanyReviewRepository reviewRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork) : ICompanyReviewService
{
    public async Task<IReadOnlyList<CompanyReviewResponseDto>> GetCompanyReviewsAsync(
        Guid companyId)
    {
        _ = await companyRepository.GetByIdAsync(companyId)
            ?? throw new NotFoundException("Company", companyId);

        var reviews = await reviewRepository
            .GetByCompanyIdAsync(companyId);

        return reviews.Select(r => r.ToDto()).ToList();
    }

    public async Task CreateReviewAsync(
        Guid companyId, CreateCompanyReviewDto dto, Guid userId)
    {
        _ = await companyRepository.GetByIdAsync(companyId)
            ?? throw new NotFoundException("Company", companyId);

        var review = dto.ToEntity(companyId, userId);

        await reviewRepository.AddAsync(review);
        await unitOfWork.SaveChangesAsync();
    }
}