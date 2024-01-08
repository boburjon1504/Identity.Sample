using Identity.Application.Common.Identity.Models;

namespace Identity.Application.Common.Identity.Services;

public interface IAuthService
{
    ValueTask<bool> RegisterAsync(RegisterDetails registerDetails);

    ValueTask<string> LoginAsync(LoginDetails loginDetails);

    ValueTask<bool> GrandRoleAsync(Guid userId, string roleType, Guid actionUserId);
}