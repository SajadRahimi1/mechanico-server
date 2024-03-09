using System.ComponentModel.DataAnnotations.Schema;

namespace Mechanico_Api.Entities;

public class User : BaseEntity
{
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? ImageUrl { get; set; }
    public bool? IsMale { get; set; }
    public List<Comment> Comments { get; set; } = new();
    public List<Visited> Visiteds { get; set; } = new();

}