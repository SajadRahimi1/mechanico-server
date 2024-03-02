namespace Mechanico_Api.Entities;

public class User : BaseEntity
{
    public String Name { get; set; }
    public String PhoneNumber { get; set; }
    public String Email { get; set; }
    public String ImageUrl { get; set; }
    public bool IsMale { get; set; }
    public List<Comments> Comments { get; set; }
}