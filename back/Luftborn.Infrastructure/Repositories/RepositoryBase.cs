using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore; 
using Luftborn.Infrastructure.Data;
using Luftborn.Core.Repositories;
using Luftborn.Core.Common;

namespace Luftborn.Infrastructure.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
{
    protected readonly LuftbornContext _dbContext;

    public RepositoryBase(LuftbornContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().Where(predicate).ToListAsync();
    }
    public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _dbContext.Set<T>().Where(predicate);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}