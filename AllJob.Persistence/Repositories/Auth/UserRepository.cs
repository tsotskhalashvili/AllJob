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
