namespace Mechanico_Api.Entities;

public class SmsCode:BaseEntity
{
    public string? Code { get; set; }
    public Guid? ReceiverId { get; set; }
}