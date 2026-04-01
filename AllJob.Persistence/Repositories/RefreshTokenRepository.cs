using AllJob.Application.Interfaces.Repositories;
using AllJob.Domain.Entities.Auth;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories;

public class RefreshTokenRepository(AppDbContext context)
    : GenericRepository<RefreshToken>(context), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenAsync(string token)
        => await _dbSet
            .FirstOrDefaultAsync(t => t.Token == token);
}