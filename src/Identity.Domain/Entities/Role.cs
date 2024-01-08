using Identity.Domain.Common;
using Identity.Domain.Enums;

namespace Identity.Domain.Entities;

public class Role : IEntity
{
    public Guid Id { get; set; }

    public RoleType Type { get; set; } = RoleType.Host;

    public bool IsDisabled { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public DateTime? UpdatedTime { get; set; }
}