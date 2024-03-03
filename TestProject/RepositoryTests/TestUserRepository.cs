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

    public TestUserRepository()
    {
        var appDbContext = new MockAppDbContext().AppDbContext;
        _users = appDbContext.Users.ToList();
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
}