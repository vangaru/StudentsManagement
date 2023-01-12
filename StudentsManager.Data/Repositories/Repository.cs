using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudentsManagement.Domain.Repositories;
using StudentsManager.Data.Data;

namespace StudentsManager.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly StudentsManagementContext _studentsManagementContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(StudentsManagementContext studentsManagementContext)
    {
        _studentsManagementContext = studentsManagementContext;
        _dbSet = studentsManagementContext.Set<TEntity>();
    }
    
    public async Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetEntityWithIncludeAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        cancellationToken.ThrowIfCancellationRequested();
        IQueryable<TEntity> query = Include(includeProperties);
        return await query.FirstAsync(predicate, cancellationToken);
    }

    private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;
        return includeProperties
            .Aggregate(query, (current, property) => current.Include(property));
    }

    public async Task<int> DeleteEntityAsync(Guid entityId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        TEntity? entity = await _dbSet.FindAsync(entityId);
        
        if (entity is null)
        {
            throw new KeyNotFoundException($"Entity with the specified key({entityId}) is not found in the database");
        }

        _dbSet.Remove(entity);
        return await _studentsManagementContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<int> CreateEntityAsync(TEntity entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await _dbSet.AddAsync(entity, cancellationToken);
        return await _studentsManagementContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _dbSet.Update(entity);
        return await _studentsManagementContext.SaveChangesAsync(cancellationToken);
    }
}