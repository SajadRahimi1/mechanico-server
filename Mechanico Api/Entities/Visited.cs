namespace Mechanico_Api.Entities;

public class Visited : BaseEntity
{
    public User User { get; set; }
    public Guid UserId { get; set; }

    public Mechanic Mechanic { get; set; }
    public Guid MechanicId { get; set; }
}