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

    [HttpGet,Route("/send-code")]
    public async Task<ActionResult> SendCode([FromQuery] string phoneNumber)
    {
        return await _userRepository.SendCode(phoneNumber);
    }
}