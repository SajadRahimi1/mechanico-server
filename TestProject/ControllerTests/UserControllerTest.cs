using System.ComponentModel.DataAnnotations;
using Mechanico_Api.Controllers;
using Mechanico_Api.Dtos;
using Mechanico_Api.Repositories;

namespace TestProject.ControllerTests;

public class UserControllerTest
{
    private readonly UserController _userController;

    public UserControllerTest()
    {
        var appDbContext = new MockAppDbContext().AppDbContext;
       var _userRepository = new UserRepository(appDbContext,new SmsCodeRepository(appDbContext));
       _userController = new(_userRepository);
    }

    [Fact]
    public async void TestSendSmsWithFailedValidation()
    {
        var result = await _userController.SendCode(new SendCodeDto { phoneNumber = "sssssssss" });
        Assert.Equal(400,result.Result.StatusCode);
    }
}