using Identity.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Repositories;

public abstract class EntityBaseRepository<TEntity, TContext> where TEntity : class, IEntity
    where TContext : DbContext

{
    protected DbSet<TEntity> DbList => _dbSet;
    protected DbContext DbContext => _dbContext;


    private readonly DbSet<TEntity> _dbSet;
    private readonly DbContext _dbContext;

    protected EntityBaseRepository(DbContext dbContext)
    {
        _dbContext = (TContext)dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }
    public IQueryable<TEntity> Get(bool asNoTracking = true)
    {
        if (asNoTracking)
            DbList.AsNoTracking();
        
        return DbList.AsQueryable();
    }

    public async ValueTask<TEntity?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        if (!asNoTracking)
            DbList.AsNoTracking();

        return await DbList.SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async ValueTask<TEntity> CreateAsync(TEntity entity, bool saveChanges = true,CancellationToken cancellationToken = default)
    {
        await DbList.AddAsync(entity,cancellationToken);

        if(saveChanges)
             await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
    public async ValueTask<TEntity> Update(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        DbList.Update(entity);

        if(saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async ValueTask<TEntity> Delete(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {

        DbList.Remove(entity);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
