using AllJob.Application.Interfaces;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace AllJob.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    public async Task<int> SaveChangesAsync()
        => await context.SaveChangesAsync();

    public async Task BeginTransactionAsync()
        => _transaction = await context.Database
            .BeginTransactionAsync();

    public async Task CommitAsync()
    {
        try
        {
            if (_transaction is not null)
                await _transaction.CommitAsync();
        }
        catch
        {
           
            await RollbackAsync();
            throw;
        }
        finally
        {
          
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction is not null)
            await _transaction.RollbackAsync();

        _transaction?.Dispose();
        _transaction = null;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        context.Dispose();
    }
}