using AllJob.Application.DTOs.Admin;
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
        var user = await userRepository.GetByIdAsync(userId)
            ?? throw new NotFoundException("User", userId);

        userRepository.Delete(user);
        await unitOfWork.SaveChangesAsync();
    }
    public async Task<IReadOnlyList<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllCandidatesAsync(); 
        return users.Select(u => u.ToDto()).ToList();
    }
}
