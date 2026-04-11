using AllJob.Domain.Entities.Auth;

namespace AllJob.Application.Interfaces.Repositories.Auth;

public interface IPasswordResetTokenRepository
{

    Task AddAsync(PasswordResetToken token);
    Task<PasswordResetToken?> GetByTokenAsync(string token);
    Task<PasswordResetToken?> GetActiveTokenByUserIdAsync(Guid userId);
    void Update(PasswordResetToken token);
}