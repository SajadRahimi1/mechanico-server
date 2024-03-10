namespace Mechanico_Api.Entities;

public class Mechanic:BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? StoreName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CallNumber { get; set; }
    public string? City { get; set; }
    public string? Password { get; set; }
    public Address Address { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public List<Visited> Visiteds { get; set; } = new();
}