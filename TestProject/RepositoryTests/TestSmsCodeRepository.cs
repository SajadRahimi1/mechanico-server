using Mechanico_Api.Entities;
using Mechanico_Api.Repositories;

namespace TestProject.RepositoryTests;

public class TestSmsCodeRepository
{
    private readonly SmsCodeRepository _smsCodeRepository;
    private readonly IList<SmsCode> _smsCodes;

    public TestSmsCodeRepository()
    {
        var appDbContext= new MockAppDbContext().AppDbContext;
        _smsCodes = appDbContext.SmsCodes.ToList();
        _smsCodeRepository = new SmsCodeRepository(appDbContext);
    }

    [Fact]
    public async void TestGetSmsCodeByReceiverId()
    {
        var smsCode = _smsCodeRepository.GetSmsByReceiverId(_smsCodes[0].ReceiverId ?? Guid.NewGuid());
        Assert.Equal(_smsCodes[0].Id,smsCode?.Id);
    }

    [Fact]
    public async void TestCount()
    {
        Assert.Equal("1244",new MockAppDbContext().AppDbContext.SmsCodes.ToList()[0].Code);
    }

    [Fact]
    public async void SendCodeFirstTime()
    {
        var result =await _smsCodeRepository.SendCode(Guid.NewGuid());
        Assert.Equal(201, result.Result.StatusCode);
    }

 /*   [Fact]
    public async void SendCodeForRegisteredUser()
    {
        var result =await _smsCodeRepository.SendCode(_smsCodes[0].ReceiverId??Guid.NewGuid());
        Assert.Equal(202, result.Result.StatusCode);
    }*/
    
    [Fact]
    public async void SendCodeTwiceInTwoMinute()
    {
        var receiverId = _smsCodes[1].ReceiverId ?? Guid.NewGuid();
        await _smsCodeRepository.SendCode(receiverId);
        var result = await _smsCodeRepository.SendCode(receiverId);
        Assert.Equal(200, result.Result.StatusCode);
    }

}