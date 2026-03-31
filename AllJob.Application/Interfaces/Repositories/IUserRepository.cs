using AllJob.Domain.Entities.Auth;

namespace AllJob.Application.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);

}
