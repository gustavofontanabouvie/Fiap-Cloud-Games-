using Microsoft.EntityFrameworkCore;
using Payments.Data.Context;
using Payments.Data.Repositories.Interface;
using System.Linq.Expressions;


namespace Payments.Data.Repositories.Implementation;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly PaymentsDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(PaymentsDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async virtual Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        return entity;
    }

    public virtual Task Delete(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);

        return Task.CompletedTask;
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual Task Update(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);

        return Task.CompletedTask;
    }


}
