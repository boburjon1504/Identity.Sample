﻿using Identity.Domain.Entities;
using Identity.Domain.Enums;

namespace Identity.Application.Common.Identity.Services;

public interface IRoleService
{
    ValueTask<Role> GetByTypeAsync(RoleType type, bool asNoTracking = false, CancellationToken cancellationToken = default);
}