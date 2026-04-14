using AllJob.Domain.Entities.Auth;

namespace AllJob.Application.Interfaces.Repositories.Auth;

public interface IAdminInviteRepository
{
    Task AddAsync(AdminInvite invite);
    Task<AdminInvite?> GetByTokenHashAsync(string tokenHash);
    void Update(AdminInvite invite);

}
