using AllJob.Application.Interfaces.Repositories.Subscriptions;
using AllJob.Domain.Entities.Subscriptions;
using AllJob.Persistence.Context;
using AllJob.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Subscriptions;

public class PaymentRepository(AppDbContext context)
    : GenericRepository<Payment>(context), IPaymentRepository

{
    public async Task<Payment?> GetByIdWithDetailsAsync(Guid id)
      => await _dbSet
        .Include(p => p.Company)
        .Include(p => p.Plan)
        .FirstOrDefaultAsync(p => p.Id == id);
}
