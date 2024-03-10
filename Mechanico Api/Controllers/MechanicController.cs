using Mechanico_Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ActionResult = Mechanico_Api.Contexts.ActionResult;

namespace Mechanico_Api.Controllers;

[Route("[controller]"),ApiController()]
public class MechanicController:ControllerBase
{
    private readonly IMechanicRepository _mechanicRepository;

    public MechanicController(IMechanicRepository mechanicRepository)
    {
        _mechanicRepository = mechanicRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetMechanicById([FromQuery] Guid mechanicId) =>
        await _mechanicRepository.GetMechanic(mechanicId);
    
    [HttpGet,Route("city")]
    public async Task<ActionResult> GetMechanicByCity([FromQuery] string city) =>
        await _mechanicRepository.GetMechanicByCity(city);
}