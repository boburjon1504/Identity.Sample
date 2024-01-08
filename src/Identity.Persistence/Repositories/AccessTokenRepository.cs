using Identity.Domain.Entities;
using Identity.Persistence.DataContext;
using Identity.Persistence.Repositories.Interfaces;

namespace Identity.Persistence.Repositories;

public class AccessTokenRepository : EntityRepositoryBase<AccessToken, AppDbContext>, IAccessTokenRepository
{
    public AccessTokenRepository(AppDbContext context) : base(context)
    {
    }

    public new async ValueTask<AccessToken> CreateAsync(AccessToken token, bool saveChanges = true, CancellationToken cancellationToken = default) =>
        await base.CreateAsync(token, saveChanges, cancellationToken);
}