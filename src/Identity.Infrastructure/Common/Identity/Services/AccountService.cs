using Identity.Application.Common.Enums;
using Identity.Application.Common.Identity.Services;
using Identity.Application.Common.Notifications.Services;
using Identity.Domain.Entities;
using Identity.Domain.Enums;

namespace Identity.Infrastructure.Common.Identity.Services;

public class AccountService : IAccountService
{
    private readonly IVerificationTokenGeneratorService _verificationTokenGeneratorService;
    private readonly IEmailOrchestrationService _emailOrchestrationService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IPasswordHasherService _passwordHasherService;

    public AccountService(IPasswordHasherService passwordHasherService, IRoleService roleService, IVerificationTokenGeneratorService verificationTokenGeneratorService, IEmailOrchestrationService emailOrchestrationService, IUserService userService)
    {
        _verificationTokenGeneratorService = verificationTokenGeneratorService;
        _emailOrchestrationService = emailOrchestrationService;
        _userService = userService;
        _passwordHasherService = passwordHasherService;
        _roleService = roleService;
    }

    public async ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        var role = (await _roleService.GetByTypeAsync(RoleType.Guest))!;

        user.RoleId = role.Id;
        user.Role = role;
        user.Password = _passwordHasherService.PasswordHasher(user.Password);


        var createdUser = await _userService.CreateAsync(user, true, cancellationToken);

        var verificaitonToken = _verificationTokenGeneratorService.GenerateToken(VerificationType.EmailAddressVerificaton, createdUser.Id);

        var verificationEmailResult = await _emailOrchestrationService.SendMessageAsync(user.Email, $"Siz saytga hush kelibsiz - " +
            $"{verificaitonToken}");

        var result = verificationEmailResult;

        return result;
    }

    public ValueTask<bool> VerificationAsync(string token, CancellationToken cancellationToken = default)
    {
        if(token is null)
            throw new ArgumentNullException("invalid token");

        var verificationTokenResult = _verificationTokenGeneratorService.DecodeToken(token);

        if(!verificationTokenResult.IsValid)
            throw new InvalidOperationException("Invalid token");

        var result = verificationTokenResult.Token.Type switch
        {
            VerificationType.EmailAddressVerificaton => MarkAsEmailVerifiedAsync(verificationTokenResult.Token.UserId, VerificationType.EmailAddressVerificaton),
            _ => throw new InvalidOperationException("This method is not intended to accept other types of tokens")
        };

        return result;
    }

    public ValueTask<bool> MarkAsEmailVerifiedAsync(Guid userId, VerificationType verificationType)
    {
        return new(true);
    }
}