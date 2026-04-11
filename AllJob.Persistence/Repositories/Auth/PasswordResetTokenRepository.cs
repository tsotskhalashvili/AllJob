using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Domain.Entities.Auth;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Auth
{
    public class PasswordResetTokenRepository(AppDbContext context) 
        : IPasswordResetTokenRepository
    {
        public async Task AddAsync(PasswordResetToken token)
          => await context.PasswordResetTokens.AddAsync(token);
        public async Task<PasswordResetToken?> GetByTokenAsync(string token)
           => await context.PasswordResetTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Token == token);
        public async Task<PasswordResetToken?> GetActiveTokenByUserIdAsync(Guid userId)
            => await context.PasswordResetTokens
            .FirstOrDefaultAsync(t =>
                t.UserId == userId &&
                !t.IsUsed &&
                t.ExpiresAt > DateTime.UtcNow);


        public void Update(PasswordResetToken token)
        => context.PasswordResetTokens.Update(token);


    }
}
