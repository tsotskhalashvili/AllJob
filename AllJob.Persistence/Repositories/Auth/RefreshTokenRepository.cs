using AllJob.Application.Helpers;
using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Application.Settings;
using AllJob.Domain.Entities.Auth;
using AllJob.Persistence.Context;
using AllJob.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AllJob.Persistence.Repositories.Auth;

public class RefreshTokenRepository(AppDbContext context,
     IOptions<TokenHashSettings> tokenHashSettings)
    : GenericRepository<RefreshToken>(context), IRefreshTokenRepository
{
    private readonly string _secret = tokenHashSettings.Value.Secret;

    public async Task<RefreshToken?> GetByTokenAsync(string token)
        => await _dbSet
            .FirstOrDefaultAsync(t =>
                t.TokenHash == TokenHasher.Hash(token, _secret));
}