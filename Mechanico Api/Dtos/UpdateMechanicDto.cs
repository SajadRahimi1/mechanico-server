namespace Mechanico_Api.Dtos;

public class UpdateMechanicDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? StoreName { get; set; }
    public string? CallNumber { get; set; }
    
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}