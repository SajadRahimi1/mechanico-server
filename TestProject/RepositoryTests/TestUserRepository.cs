using Mechanico_Api.Contexts;
using Mechanico_Api.Entities;
using Mechanico_Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NSubstitute;

namespace TestProject.RepositoryTests;

public class TestUserRepository
{
    private readonly UserRepository _userRepository;
    private readonly IList<User> _users;
    private readonly IList<SmsCode> _smsCodes;

    public TestUserRepository()
    {
        var appDbContext = new MockAppDbContext().AppDbContext;
        _users = appDbContext.Users.ToList();
        _smsCodes = appDbContext.SmsCodes.ToList();
        _userRepository = new UserRepository(appDbContext,new SmsCodeRepository(appDbContext));
    }

    [Fact]
    public void TestGetUserByPhoneNumber()
    {
        var user = _userRepository.GetUserByPhoneNumber(_users[0].PhoneNumber??"");
        Assert.Equal(_users[0].Id,user?.Id);
    }

    [Fact]
    public async void TestSendSmsCode()
    {
        var result = await _userRepository.SendCode("09214961842");
        Assert.InRange(result.Result.StatusCode,200,202);
    }

    [Fact]
    public async void TestCheckCodeSuccess()
    {
        var result = await _userRepository.CheckCode(_users[0].PhoneNumber??"", "1244");
        Assert.Equal(200,result.Result.StatusCode);
    }

    [Fact]
    public async void TestGetByIdFromSmsCode()
    {
        var user = await  _userRepository.GetUserById(_smsCodes[0].ReceiverId??Guid.NewGuid());
        Assert.Equal(DataHelper.Instance.GetUsers()[0].Id,user?.Id);
    }
}