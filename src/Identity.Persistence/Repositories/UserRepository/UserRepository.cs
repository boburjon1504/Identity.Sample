using Identity.Domain.Entities;
using Identity.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Identity.Persistence.Repositories.UserRepository;

public class UserRepository : EntityBaseRepository<User, AppDbContext>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public new IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, bool asNoTracking = false, CancellationToken cancellationToken = default) => base.Get(asNoTracking);

    public new ValueTask<User?> GetByIdAsync(Guid id,bool asNoTracking = true) => 
        base.GetByIdAsync(id, asNoTracking);

    public ValueTask<User> CreateAsync(User user,bool saveChanges = true) =>
        base.CreateAsync(user,saveChanges);
   
    public ValueTask<User> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default) =>
        base.Delete(user,saveChanges);

    public async ValueTask<User?> GetByEmailAsync(string email, bool asNoTracking = true)
    {
        if (asNoTracking)
            DbList.AsNoTracking();

        return await DbList
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default) =>
      base.Update(user, saveChanges);
    
}
