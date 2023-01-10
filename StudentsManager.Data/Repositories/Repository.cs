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
    
    public async Task<IEnumerable<TEntity>> GetEntitiesAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }
}