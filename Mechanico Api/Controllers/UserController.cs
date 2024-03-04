using Mechanico_Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mechanico_Api.Controllers;

[ApiController()]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost,Route("send")]
    public async Task<IActionResult> SendCode([FromBody] string phoneNumber)
    {
        return await _userRepository.SendCode(phoneNumber);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await _userRepository.GetAll();
    }
}