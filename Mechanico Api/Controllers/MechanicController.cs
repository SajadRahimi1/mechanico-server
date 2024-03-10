using System.Security.Claims;
using AutoMapper;
using Mechanico_Api.Dtos;
using Mechanico_Api.Entities;
using Mechanico_Api.Interfaces;
using Mechanico_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ActionResult = Mechanico_Api.Contexts.ActionResult;

namespace Mechanico_Api.Controllers;

[Route("[controller]"),ApiController()]
public class MechanicController:ControllerBase
{
    private readonly IMechanicRepository _mechanicRepository;
    private readonly IMapper _mapper;
    private readonly IJwtRepository _jwtRepository;

    public MechanicController(IMechanicRepository mechanicRepository, IMapper mapper, IJwtRepository jwtRepository)
    {
        _mechanicRepository = mechanicRepository;
        _mapper = mapper;
        _jwtRepository = jwtRepository;
    }

    private JwtModel? AuthorizeMechanic()
    {
        return HttpContext.User.Identity is not ClaimsIdentity identity ? null : _jwtRepository.Authorize(identity);
    }

    [HttpGet]
    public async Task<ActionResult> GetMechanicById([FromQuery] Guid mechanicId) =>
        await _mechanicRepository.GetMechanic(mechanicId);
    
    [HttpGet,Route("city")]
    public async Task<ActionResult> GetMechanicByCity([FromQuery] string city) =>
        await _mechanicRepository.GetMechanicByCity(city);
    
    [HttpPost, Route("send-code")]
    public async Task<ActionResult> SendCode([FromBody] SendCodeDto sendCodeDto)
    {
        return await _mechanicRepository.SendCode(sendCodeDto.phoneNumber);
    }

    [HttpPost, Route("validate-code")]
    public async Task<ActionResult> ValidateSmsCode([FromBody] ValidateCodeDto validateCodeDto) =>
        await _mechanicRepository.CheckCode(validateCodeDto.phoneNumber, validateCodeDto.code);
   
    [HttpPost, Route("update"),Authorize(Roles = "mechanic")]
    public async Task<ActionResult> UpdateMechanic([FromBody] UpdateMechanicDto updateMechanicDto)
    {
        var mechanic = _mapper.Map<Mechanic>(updateMechanicDto);
        mechanic.Id=Guid.Parse(AuthorizeMechanic()?.Id);
        return await _mechanicRepository.UpdateMechanic(mechanic);
    }
}