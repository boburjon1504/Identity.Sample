using Identity.Domain.Entities;
using Identity.Persistence.DataContext;
using Identity.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Identity.Persistence.Repositories;

public class RoleRepository : EntityRepositoryBase<Role, AppDbContext>, IRoleRepository
{
    public RoleRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<Role?> Get(Expression<Func<Role, bool>>? predicate = default, bool asNoTracking = default) =>
        base.Get(predicate, asNoTracking);
}