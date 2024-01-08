using Identity.Domain.Entities;

namespace Identity.Application.Common.Identity.Services;

public interface IAccessTokenService
{
    public ValueTask<AccessToken> CreateAsync(Guid userId, string value, bool saveChanges = true, CancellationToken cancellationToken = default);
}