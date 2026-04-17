using AllJob.Application.Helpers;
using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Application.Settings;
using AllJob.Domain.Entities.Auth;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AllJob.Persistence.Repositories.Auth;

public class PasswordResetTokenRepository(
    AppDbContext context,
    IOptions<TokenHashSettings> tokenHashSettings)
    : IPasswordResetTokenRepository
{
    private readonly string _secret = tokenHashSettings.Value.Secret;

    public async Task AddAsync(PasswordResetToken token)
        => await context.PasswordResetTokens.AddAsync(token);

    public async Task<PasswordResetToken?> GetByTokenAsync(string token)
        => await context.PasswordResetTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t =>
                t.TokenHash == TokenHasher.Hash(token, _secret));

    public async Task<PasswordResetToken?> GetActiveTokenByUserIdAsync(Guid userId)
        => await context.PasswordResetTokens
            .FirstOrDefaultAsync(t =>
                t.UserId == userId &&
                !t.IsUsed &&
                t.ExpiresAt > DateTime.UtcNow);

    public void Update(PasswordResetToken token)
        => context.PasswordResetTokens.Update(token);

    public async Task DeleteExpiredTokensAsync()
    => await context.PasswordResetTokens
        .Where(t => t.ExpiresAt < DateTime.UtcNow || t.IsUsed)
        .ExecuteDeleteAsync();
}