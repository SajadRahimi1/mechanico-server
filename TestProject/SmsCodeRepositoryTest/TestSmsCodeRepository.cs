using Mechanico_Api.Contexts;
using Mechanico_Api.Entities;
using Mechanico_Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace TestProject.SmsCodeRepositoryTest;

public class TestSmsCodeRepository
{
    private readonly SmsCodeRepository _smsCodeRepository;
    private readonly IList<SmsCode> _smsCodes;

    public TestSmsCodeRepository()
    {
        var mock = new Mock<AppDbContext>(new DbContextOptionsBuilder().UseSqlServer().Options);
        _smsCodes = new List<SmsCode>()
        {
            new SmsCode()
            {
                ReceiverId = Guid.Parse("d5a0059b-0fea-4d6f-9ad4-d3595620b924"),
                Code = "1244"
            },
            new SmsCode()
            {
                ReceiverId = Guid.Parse("271046ca-fcde-4626-8165-0cb4906227ef"),
                Code = "1445",
                
            }
        };
        mock.Setup(s=>s.SmsCodes).ReturnsDbSet(_smsCodes);
        
        _smsCodeRepository=new SmsCodeRepository(mock.Object);
    }

    [Fact]
    public async void TestGetSmsCodeByReceiverId()
    {
        var smsCode = _smsCodeRepository.GetSmsByReceiverId(_smsCodes[0].ReceiverId ?? Guid.NewGuid());
        Assert.Equal(_smsCodes[0].Id,smsCode?.Id);
    }
    
    [Fact]
    public async void SendCodeFirstTime()
    {
        var result =await _smsCodeRepository.SendCode(Guid.NewGuid());
        Assert.Equal(201, result.Result.StatusCode);
    }

    [Fact]
    public async void SendCodeForRegisteredUser()
    {
        var result =await _smsCodeRepository.SendCode(_smsCodes[0].ReceiverId??Guid.NewGuid());
        Assert.Equal(202, result.Result.StatusCode);
    }
    
    [Fact]
    public async void SendCodeTwiceInTwoMinute()
    {
        var receiverId = _smsCodes[1].ReceiverId ?? Guid.NewGuid();
        await _smsCodeRepository.SendCode(receiverId);
        var result = await _smsCodeRepository.SendCode(receiverId);
        Assert.Equal(200, result.Result.StatusCode);
    }

}