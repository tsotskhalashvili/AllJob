using AllJob.Application.DTOs.Common;
using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Domain.Entities.Auth;
using AllJob.Persistence.Context;
using AllJob.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Auth
{
    public class UserRepository(AppDbContext context)
        : GenericRepository<User>(context), IUserRepository
    {

        public async Task<PagedResponseDto<User>> GetAllCandidatesAsync(int page, int pageSize)
        {
            var query = _dbSet
                .AsNoTracking()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.UserRoles
                    .Any(ur => ur.Role.Name == "Candidate"));

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponseDto<User>(items, totalCount, page, pageSize);
        }

        public async Task<PagedResponseDto<User>> GetAllEmployersAsync(int page, int pageSize)
        {
            var query = _dbSet
                .AsNoTracking()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.UserRoles
                    .Any(ur => ur.Role.Name == "Employer"));

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponseDto<User>(items, totalCount, page, pageSize);
        }

        public async Task<PagedResponseDto<User>> GetAllAdminsAsync(int page, int pageSize)
        {
            var query = _dbSet
                .AsNoTracking()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(u => u.AdminProfile)
                .Where(u => u.UserRoles
                    .Any(ur => ur.Role.Name == "Admin"));

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponseDto<User>(items, totalCount, page, pageSize);
        }

        public async Task<User?> GetAdminByIdAsync(Guid id)
            => await _dbSet
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u =>
                    u.Id == id &&
                    u.UserRoles.Any(ur => ur.Role.Name == "Admin"));
        public async Task<User?> GetByEmailAsync(string email)
         => await _dbSet
             .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByEmailWithRolesAsync(string email)
         => await _dbSet
            .Include(u => u.UserRoles)
              .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByIdWithRolesAsync(Guid id)
    => await _dbSet
        .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
        .FirstOrDefaultAsync(u => u.Id == id);
    }
}
