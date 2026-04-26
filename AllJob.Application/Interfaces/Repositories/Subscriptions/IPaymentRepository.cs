using AllJob.Domain.Entities.Subscriptions;

namespace AllJob.Application.Interfaces.Repositories.Subscriptions;

public interface IPaymentRepository : IGenericRepository<Payment>
{
    Task<Payment?> GetByIdWithDetailsAsync(Guid id);
}