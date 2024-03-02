namespace Mechanico_Api.Entities;

public class Comment : BaseEntity
{
    public string? Content { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Mechanic Mechanic { get; set; }
    public Guid MechanicId { get; set; }
    public int Star { get; set; }
    public CommentStatus CommentStatus { get; set; } = CommentStatus.Unknown;
}

public enum CommentStatus
{
    Accepted,
    Rejected,
    Unknown
}