using System.Text.Json.Serialization;

namespace Mechanico_Api.Entities;
// TODO: create status prop and description for status
// TODO: create prop for store license image 
public class Mechanic:BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? StoreName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CallNumber { get; set; }
    
    [JsonIgnore]
    public string? Password { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public List<Comment> Comments { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public List<Visited> Visiteds { get; set; } = new();
}