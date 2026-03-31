using AllJob.Application.Interfaces.Repositories;
using AllJob.Domain.Entities.Auth;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories
{
    public class UserRepository(AppDbContext context)
        : GenericRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetByEmailAsync(string email)
         => await _dbSet
             .FirstOrDefaultAsync(u => u.Email == email);
    }
}
