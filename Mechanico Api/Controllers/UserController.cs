using System.Security.Claims;
using Mechanico_Api.Contexts;
using Mechanico_Api.Dtos;
using Mechanico_Api.Interfaces;
using Mechanico_Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mechanico_Api.Controllers;

[ApiController()]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtRepository _jwtRepository;


    public UserController(IUserRepository userRepository, IJwtRepository jwtRepository)
    {
        _userRepository = userRepository;
        _jwtRepository = jwtRepository;
    }

    [HttpPost, Route("send-code")]
    public async Task<Contexts.ActionResult> SendCode([FromBody] SendCodeDto sendCodeDto)
    {
        return await _userRepository.SendCode(sendCodeDto.phoneNumber);
    }

    [HttpGet]
    public async Task<Contexts.ActionResult> GetAll()
    {
        return await _userRepository.GetAll();
    }

    [HttpPost, Route("validate-code")]
    public async Task<Contexts.ActionResult> ValidateSmsCode([FromBody] ValidateCodeDto validateCodeDto) =>
        await _userRepository.CheckCode(validateCodeDto.phoneNumber, validateCodeDto.code);

    [HttpPost, Authorize(Roles = "User")]
    public Contexts.ActionResult AuthorizeUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is null)
        {
            return new Contexts.ActionResult(new Result { StatusCode = 403 });
        }

        var jwtModel = _jwtRepository.Authorize(identity);
        return new Contexts.ActionResult(new Result { Data = jwtModel });
    }
}