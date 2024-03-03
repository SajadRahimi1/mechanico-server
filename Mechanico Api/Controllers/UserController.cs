using Mechanico_Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ActionResult = Mechanico_Api.Contexts.ActionResult;

namespace Mechanico_Api.Controllers;

[ApiController(), Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult> SendCode(string phoneNumber)
    {
        return await _userRepository.SendCode(phoneNumber);
    }
}