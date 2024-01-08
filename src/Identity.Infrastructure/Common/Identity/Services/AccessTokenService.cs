using Identity.Application.Common.Identity.Services;
using Identity.Domain.Entities;
using Identity.Persistence.Repositories.Interfaces;

namespace Identity.Infrastructure.Common.Identity.Services;

public class AccessTokenService : IAccessTokenService
{
    private readonly IAccessTokenRepository _accessTokenRepository;

    public AccessTokenService(IAccessTokenRepository accessTokenRepository) =>
        _accessTokenRepository = accessTokenRepository;

    public async ValueTask<AccessToken> CreateAsync(Guid userId, string value, bool saveChanges = true, CancellationToken cancellationToken = default) =>
        await _accessTokenRepository.CreateAsync(new AccessToken
        {
            UserId = userId,
            Value = value,
            CreatedTime = DateTime.UtcNow
        },
            saveChanges,
            cancellationToken);

    public async ValueTask<AccessToken> CreateAsync(AccessToken token, bool saveChanges = true, CancellationToken cancellationToken = default) =>
        await _accessTokenRepository.CreateAsync(token, saveChanges, cancellationToken);
}