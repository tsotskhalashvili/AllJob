using AllJob.Domain.Entities.Auth;

namespace AllJob.Application.Interfaces.Repositories.Auth;

public interface IAdminInviteRepository
{
    Task AddAsync(AdminInvite invite);
    Task<AdminInvite?> GetByTokenHashAsync(string tokenHash);
    Task<AdminInvite?> GetActiveInviteByEmailAsync(string email);
    void Update(AdminInvite invite);
    Task DeleteExpiredInvitesAsync();

}
