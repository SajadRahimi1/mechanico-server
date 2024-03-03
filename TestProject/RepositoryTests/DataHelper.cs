using Mechanico_Api.Entities;

namespace TestProject.RepositoryTests;

public class DataHelper
{
    public IList<User> GetUsers()=>new List<User>()
    {
        new User()
        {
            PhoneNumber = "09214961842",
        },
        new User()
        {
            PhoneNumber = "09120000101",
        }
    };
    
    public IList<SmsCode> GetSmsCodes()=>new List<SmsCode>()
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