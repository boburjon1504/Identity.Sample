namespace Identity.Domain.Entities.Common;

public class Auditable : IEntity
{
    public Guid Id { get ; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set;}
}
