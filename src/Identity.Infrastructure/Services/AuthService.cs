using Identity.Application.Services;
using Identity.Domain.DTOs;
using Identity.Domain.Entities;
using Identity.Domain.Enums;

namespace Identity.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenGeneratorService _tokenGeneratorService;
    public AuthService(IUserService userService, ITokenGeneratorService tokenGeneratorService)
    {
        _userService = userService;
        _tokenGeneratorService = tokenGeneratorService;
    }

    public async  ValueTask<bool> RegisterAsync(User user, CancellationToken cancellationToken)
    {
        await _userService.CreateAsync(user);

        return true;
    }
    public async ValueTask<string> LodinAsync(UserForLogin user, CancellationToken cancellationToken)
    {
        var usr = await _userService.GetByEmailAsync(user.Email);

        if (usr is null)
            return null;

        return _tokenGeneratorService.GetToken(usr);
    }
    public async  ValueTask<User> GrandRoleAsync(Guid id, Role role, CancellationToken cancellationToken)
    {
        var usr = await _userService.GetByIdAsync(id,true);

        if (usr is null)
            return null;

        usr.Role = role;

        await _userService.UpdateAsync(usr, true);

        return usr;
    }
}
