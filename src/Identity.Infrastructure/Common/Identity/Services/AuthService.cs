using AutoMapper;
using Identity.Application.Common.Identity.Models;
using Identity.Application.Common.Identity.Services;
using Identity.Application.Common.Notifications.Services;
using Identity.Domain.Entities;
using Identity.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Authentication;

namespace Identity.Infrastructure.Common.Identity.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAccessTokenService _accessTokenService;
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IAccountService _accountService;
    private readonly IEmailOrchestrationService _emailOrchestrationService;

    public AuthService(IHttpContextAccessor httpContextAccessor, IAccessTokenService accessTokenService,
        IRoleService roleService, IUserService userService,
        ITokenGeneratorService tokenGeneratorService, IPasswordHasherService passwordHasherService,
        IAccountService accountService, IEmailOrchestrationService emailOrchestrationService,
        IMapper mapper)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _accessTokenService = accessTokenService;
        _roleService = roleService;
        _userService = userService;
        _tokenGeneratorService = tokenGeneratorService;
        _passwordHasherService = passwordHasherService;
        _accountService = accountService;
        _emailOrchestrationService = emailOrchestrationService;
    }

    public async ValueTask<bool> RegisterAsync(RegisterDetails registerDetails)
    {
        var found = (await _userService.Get(user => user.Email == registerDetails.EmailAddress, true)).SingleOrDefault();

        if(found is not null)
            throw new ArgumentOutOfRangeException("User already exists");


        ///var createdUser = await _userService.CreateAsync(_mapper.Map<User>(registerDetails));

        return await _accountService.CreateUserAsync(_mapper.Map<User>(registerDetails));
    }

    public async ValueTask<string> LoginAsync(LoginDetails loginDetails)
    {
        var user = await _userService.Get(user => user.Email == loginDetails.EmailAddress) ??
            throw new ArgumentNullException("User doesn't exists");
        var found = user.SingleOrDefault();

        if(found is null || !_passwordHasherService.ValidatePassword(loginDetails.Password, found.Password))
            throw new AuthenticationException("Pasword is not match");

        var token = _tokenGeneratorService.GetToken(found);

        await _accessTokenService.CreateAsync(found.Id, token);

        return token;
    }

    public async ValueTask<bool> GrandRoleAsync(Guid userId, string roleType, Guid actionUserId)
    {
        var found = await _userService.GetByIdAsync(userId) ?? throw new ArgumentNullException("User doesn't exists");
        _ = await _userService.GetByIdAsync(actionUserId) ?? throw new ArgumentNullException("User doesn't exists");

        if(Enum.TryParse(roleType, out RoleType roleValue))
            throw new ArgumentException("Countd't parse");
        var role = await _roleService.GetByTypeAsync(roleValue) ?? throw new InvalidOperationException("can't parse");

        found.RoleId = role.Id;

        await _userService.UpdateAsync(found);

        return true;
    }
}