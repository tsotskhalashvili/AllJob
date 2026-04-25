using AllJob.Application.DTOs.Common;
using AllJob.Domain.Entities.Auth;

namespace AllJob.Application.Interfaces.Repositories.Auth;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByEmailWithRolesAsync(string email);
    Task<User?> GetByIdWithRolesAsync(Guid id);
    Task<User?> GetAdminByIdAsync(Guid id);

    Task<PagedResponseDto<User>> GetAllAdminsAsync(int page, int pageSize);
    Task<PagedResponseDto<User>> GetAllCandidatesAsync(int page, int pageSize);
    Task<PagedResponseDto<User>> GetAllEmployersAsync(int page, int pageSize);
}