using Identity.Domain.Entities;

namespace Identity.Application.Common.Identity.Services;

public interface IAccountService
{
    ValueTask<bool> VerificationAsync(string token, CancellationToken cancellationToken = default);

    ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default);
}