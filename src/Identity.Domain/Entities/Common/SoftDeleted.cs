namespace Identity.Domain.Entities.Common;

public class SoftDeleted : Auditable
{
    public bool IsDeleted { get; set; } = false;
    public DateTime DeletedDate { get; set; }
}
