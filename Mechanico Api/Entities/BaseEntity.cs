using System.ComponentModel.DataAnnotations;

namespace Mechanico_Api.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}