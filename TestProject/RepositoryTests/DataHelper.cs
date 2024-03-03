using Mechanico_Api.Entities;

namespace TestProject.RepositoryTests;

public sealed class DataHelper
{
    private DataHelper(){}
    private static DataHelper _instance = null;

    public static DataHelper Instance
    {
        get { return _instance ??= new DataHelper(); }
    }
    public  IList<User> GetUsers()=>new List<User>()
    {
        new User()
        {
            PhoneNumber = "09214961842",
            Id = Guid.Parse("5f545761-6ac3-4138-98ca-e2fbd4329783")
        },
        new User()
        {
            PhoneNumber = "09120000101",
            Id = Guid.Parse("27eb33ff-f779-4c1c-9835-c5d449cdd8de")
        }
    };
    
    public  IList<SmsCode> GetSmsCodes()=>new List<SmsCode>()
    {
        new SmsCode()
        {
            ReceiverId = GetUsers()[0].Id,
            Code = "1244"
        },
        new SmsCode()
        {
            ReceiverId = GetUsers()[1].Id,
            Code = "1445",
                
        }
    };
}