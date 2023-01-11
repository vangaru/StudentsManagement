using System.Linq.Expressions;

namespace StudentsManagement.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    public Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken);

    public Task<TEntity> GetEntityWithIncludeAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken,
        params Expression<Func<TEntity, object>>[] includeProperties);

    public Task<int> CreateEntityAsync(TEntity entity, CancellationToken cancellationToken);

    public Task<int> UpdateEntityAsync(TEntity entity, CancellationToken cancellationToken);
    
    public Task<int> DeleteEntityAsync(Guid entityId, CancellationToken cancellationToken);
}