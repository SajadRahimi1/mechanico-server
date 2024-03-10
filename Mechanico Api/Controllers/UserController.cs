using System.Security.Claims;
using AutoMapper;
using Courseproject.Common.Interfaces;
using Mechanico_Api.Contexts;
using Mechanico_Api.Dtos;
using Mechanico_Api.Entities;
using Mechanico_Api.Interfaces;
using Mechanico_Api.Models;
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
    private readonly IFileRepository _fileRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IJwtRepository jwtRepository, IFileRepository fileRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtRepository = jwtRepository;
        _fileRepository = fileRepository;
        _mapper = mapper;
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

    // [HttpPost, Authorize(Roles = "User")]
    private JwtModel? AuthorizeUser()
    {
        return HttpContext.User.Identity is not ClaimsIdentity identity ? null : _jwtRepository.Authorize(identity);
    }

    [HttpPost, Authorize(Roles = "User")]
    [Route("update"), Consumes("multipart/form-data")]
    public async Task<Contexts.ActionResult> UpdateUser([FromForm] UpdateUserDto updateUserDto)
    {
        var user = _mapper.Map<User>(updateUserDto);
        var jwtModel = AuthorizeUser();
        user.Id = Guid.Parse(jwtModel.Id);
        if (updateUserDto.Image is not null)
        {
            user.ImageUrl = await _fileRepository.SaveFileAsync(updateUserDto.Image);
        }

        user = await _userRepository.UpdateUser(user);
        return new Contexts.ActionResult(new Result { Data = user });
    }

    [HttpGet, Authorize(Roles = "User")]
    [Route("visited")]
    public async Task<Contexts.ActionResult> GetUserVisited()
    {
        return await _userRepository.GetUserVisited(AuthorizeUser()?.Id??"");
    }
}