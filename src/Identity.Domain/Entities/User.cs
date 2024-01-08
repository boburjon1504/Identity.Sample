using Identity.Domain.Entities.Common;
using Identity.Domain.Enums;

namespace Identity.Domain.Entities;

public class User : SoftDeleted
{
    public string FirsName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Role Role { get; set; }
    public Guid RoleId { get; set; }
    public string Password { get; set; } = default!;

}
