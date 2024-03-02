namespace Mechanico_Api.Entities;

public class Comments:BaseEntity
{
    public string? Content { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    //TODO: define Mechanic Reference
    public int Star { get; set; }
    public CommentStatus CommentStatus { get; set; } = CommentStatus.Unknown;

}

public enum CommentStatus
{
    Accepted,
    Rejected,
    Unknown
}