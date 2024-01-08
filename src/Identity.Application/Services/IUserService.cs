using Identity.Domain.Entities;

namespace Identity.Application.Services;

public interface IUserService
{
    IQueryable<User> Get(bool asNoTracking = true);
    ValueTask<User> GetByIdAsync(Guid id, bool asNoTracking = true);
    ValueTask<User> GetByEmailAsync(string email, bool asNoTracking = true);
    ValueTask<User> CreateAsync(User user, bool saveChanges = true);
    ValueTask<User> DeleteAsync(Guid id, bool saveChanges = true);
    ValueTask<User> UpdateAsync(User user, bool saveChanges = true);
}
