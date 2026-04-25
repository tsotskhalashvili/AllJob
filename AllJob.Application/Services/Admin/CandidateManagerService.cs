using AllJob.Application.DTOs.Admin;
using AllJob.Application.DTOs.Common;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Application.Interfaces.Services.Admin;
using AllJob.Application.Mappings;

namespace AllJob.Application.Services.Admin;

public class CandidateManagerService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICandidateManagerService
{
    public async Task DeactivateUserAsync(Guid userId)
    {
        var user = await userRepository.GetByIdAsync(userId)
            ?? throw new NotFoundException("User", userId);

        user.IsActive = false;
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await userRepository.GetByIdWithRolesAsync(userId)
            ?? throw new NotFoundException("User", userId);

        var role = user.UserRoles.FirstOrDefault()?.Role.Name;
        if (role is "Admin" or "SuperAdmin")
            throw new ForbiddenException("Cannot delete admin users");

        userRepository.Delete(user);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<PagedResponseDto<UserResponseDto>> GetAllUsersAsync(int page, int pageSize)
    {
        var result = await userRepository.GetAllCandidatesAsync(page, pageSize);
        var items = result.Items.Select(u => u.ToDto()).ToList();
        return new PagedResponseDto<UserResponseDto>(items, result.TotalCount, page, pageSize);
    }
}