using AllJob.Application.DTOs.Admin;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Application.Interfaces.Repositories.Companies;
using AllJob.Application.Interfaces.Services.Admin;
using AllJob.Application.Mappings;

namespace AllJob.Application.Services.Admin;

public class AdminService(
    IUserRepository userRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork) : IAdminService
{
    public async Task<IReadOnlyList<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllAsync();
        return users.Select(u => u.ToDto()).ToList();
    }

    public async Task DeactivateUserAsync(Guid userId)
    {
        var user = await userRepository.GetByIdAsync(userId)
            ?? throw new NotFoundException("User", userId);

        user.IsActive = false;
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task VerifyCompanyAsync(Guid companyId)
    {
        var company = await companyRepository.GetByIdAsync(companyId)
            ?? throw new NotFoundException("Company", companyId);

        company.IsVerified = true;
        companyRepository.Update(company);
        await unitOfWork.SaveChangesAsync();
    }
}