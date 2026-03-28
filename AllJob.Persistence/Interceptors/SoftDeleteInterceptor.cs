using AllJob.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AllJob.Persistence.Interceptors;

public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var now = DateTime.UtcNow;

        foreach (var entry in eventData.Context.ChangeTracker
            .Entries<ISoftDeletable>()
            .Where( e => e.State == EntityState.Deleted))
        {
            entry.State = EntityState.Unchanged;

            entry.Property(x => x.IsDeleted).CurrentValue = true;
            entry.Property(x => x.IsDeleted).IsModified = true;

            entry.Property(x => x.DeletedAt).CurrentValue = now;
            entry.Property(x => x.DeletedAt).IsModified = true;

            if (entry.Entity is IAuditable)
            {
                entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = now;
                entry.Property(nameof(IAuditable.UpdatedAt)).IsModified = true;
            }


        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);

    }

}
