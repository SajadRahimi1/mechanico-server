namespace Mechanico_Api.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; }=Guid.NewGuid();
    public DateTime CreatedAt { get; }=DateTime.Now;
    public DateTime? UpdatedAt { get; } = null;

}