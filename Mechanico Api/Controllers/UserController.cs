using Mechanico_Api.Dtos;
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

    [HttpPost,Route("send-code")]
    public async Task<IActionResult> SendCode([FromBody] SendCodeDto sendCodeDto)
    {
        return await _userRepository.SendCode(sendCodeDto.phoneNumber);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await _userRepository.GetAll();
    }
}