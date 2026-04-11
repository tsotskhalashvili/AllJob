using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Domain.Entities.Auth;
using AllJob.Persistence.Context;
using AllJob.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Auth;

public class RefreshTokenRepository(AppDbContext context)
    : GenericRepository<RefreshToken>(context), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenAsync(string token)
        => await _dbSet
            .FirstOrDefaultAsync(t => t.Token == token);
}