namespace Mechanico_Api.Entities;

public class Address:BaseEntity
{
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public Guid MechanicId { get; }
    public Mechanic Mechanic { get; }
}