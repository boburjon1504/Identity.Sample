using Identity.Domain.DTOs;
using Identity.Domain.Entities;
using Identity.Domain.Enums;

namespace Identity.Application.Services;

public interface IAuthService
{
    ValueTask<bool> RegisterAsync(User user, CancellationToken cancellationToken);
    ValueTask<string> LodinAsync(UserForLogin user, CancellationToken cancellationToken);
    ValueTask<User> GrandRoleAsync(Guid id,Role role, CancellationToken cancellationToken);
}
