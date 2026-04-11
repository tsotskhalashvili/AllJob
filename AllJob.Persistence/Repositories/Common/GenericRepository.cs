using AllJob.Application.Interfaces.Repositories;
using AllJob.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AllJob.Persistence.Repositories.Common
{
    public class GenericRepository<T>(AppDbContext context) :
        IGenericRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T?> GetByIdAsync(Guid id)
         => await _dbSet.FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllAsync()
          => await _dbSet.ToListAsync();

        public async Task AddAsync(T entity)
         => await _dbSet.AddAsync(entity);

        public void Update(T entity)
         => _dbSet.Update(entity);


        public void Delete(T entity)
          => _dbSet.Remove(entity);



    }
}
