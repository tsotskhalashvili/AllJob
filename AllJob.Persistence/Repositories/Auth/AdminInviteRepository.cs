using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Domain.Entities.Auth;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Auth;

public class AdminInviteRepository(AppDbContext context) : IAdminInviteRepository
{
    public async Task AddAsync(AdminInvite invite)
     => await context.AdminInvites.AddAsync(invite);

    public async Task<AdminInvite?> GetByTokenHashAsync(string tokenHash)
    => await context.AdminInvites
        .FirstOrDefaultAsync(i =>
        i.TokenHash == tokenHash &&
        i.UsedAt == null &&
        i.ExpiresAt > DateTime.UtcNow);

    public async Task<AdminInvite?> GetActiveInviteByEmailAsync(string email)
    => await context.AdminInvites
        .FirstOrDefaultAsync(i =>
            i.Email == email &&
            i.UsedAt == null &&
            i.ExpiresAt > DateTime.UtcNow);

    public void Update(AdminInvite invite)
         => context.AdminInvites.Update(invite);
}
