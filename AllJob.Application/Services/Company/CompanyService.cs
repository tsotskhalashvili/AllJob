using AllJob.Application.DTOs.Company;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Companies;
using AllJob.Application.Interfaces.Services.Company;
using AllJob.Application.Mappings;

namespace AllJob.Application.Services.Company;

public class CompanyService(
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork) : ICompanyService
{
    public async Task<CompanyResponseDto> GetCompanyByIdAsync(Guid id)
    {
        var company = await companyRepository
            .GetCompanyWithDetailsAsync(id)
            ?? throw new NotFoundException("Company", id);

        return company.ToDto();
    }

    public async Task<CompanyResponseDto> CreateCompanyAsync(
        CreateCompanyDto dto, Guid userId)
    {
        var company = dto.ToEntity(userId);
        await companyRepository.AddAsync(company);
        await unitOfWork.SaveChangesAsync();
        return company.ToDto();
    }

    public async Task UpdateCompanyAsync(
        Guid id, UpdateCompanyDto dto, Guid userId)
    {
        var company = await companyRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Company", id);

        if (company.UserId != userId)
            throw new ForbiddenException();

        company.UpdateEntity(dto);
        companyRepository.Update(company);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCompanyAsync(Guid id, Guid userId)
    {
        var company = await companyRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Company", id);

        if (company.UserId != userId)
            throw new ForbiddenException();

        companyRepository.Delete(company);
        await unitOfWork.SaveChangesAsync();
    }
}