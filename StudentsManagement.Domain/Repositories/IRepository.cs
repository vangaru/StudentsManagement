namespace StudentsManagement.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    public Task<IEnumerable<TEntity>> GetEntitiesAsync();
}