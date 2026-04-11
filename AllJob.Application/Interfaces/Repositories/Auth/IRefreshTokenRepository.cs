using AllJob.Domain.Entities.Auth;

namespace AllJob.Application.Interfaces.Repositories.Auth;

public interface IRefreshTokenRepository
    :IGenericRepository<RefreshToken>
{

    Task<RefreshToken?> GetByTokenAsync(string token);
}
