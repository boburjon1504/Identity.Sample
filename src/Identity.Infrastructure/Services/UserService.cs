using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Persistence.Repositories.UserRepository;

namespace Identity.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public ValueTask<User> CreateAsync(User user, bool saveChanges = true)
    {
        return _userRepository.CreateAsync(user, saveChanges);
    }

    public async ValueTask<User> DeleteAsync(Guid id, bool saveChanges = true)
    {
        var deletedEntity = await _userRepository.GetByIdAsync(id);

        if (deletedEntity is null)
            return null;

        return await _userRepository.DeleteAsync(deletedEntity,saveChanges);
    }

    public IQueryable<User> Get(bool asNoTracking = true)
    {
        return _userRepository.Get(x=>true);
    }

    public ValueTask<User> GetByEmailAsync(string email, bool asNoTracking = true)
    {
        return _userRepository.GetByEmailAsync(email, asNoTracking);
    }

    public ValueTask<User> GetByIdAsync(Guid id, bool asNoTracking = true)
    {
        return _userRepository.GetByIdAsync(id,asNoTracking);
    }

    public ValueTask<User> UpdateAsync(User user, bool saveChanges = true)
    {
        return _userRepository.UpdateAsync(user,saveChanges);
    }

    
}
