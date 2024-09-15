using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using SportEvents.Domain;
using SportEvents.Enum;

namespace Web.Controllers.api;

[Route("api/[controller]")]
[ApiController]
public class RegistrationsApiController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationsApiController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }


    [HttpGet("[action]")]
    public async Task<List<Registration>> GetAllRegistrations()
    {
        return await _registrationService.GetRegistrations();
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Approve([FromBody] Guid id)
    {
        var registration = await _registrationService.GetRegistrationById(id);
        registration.Status = RegistrationStatus.Confirmed;
        var result=await _registrationService.UpdateRegistration(registration);

        if (result != null)
        {
            return Ok();
        }

        return StatusCode(500);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Reject([FromBody] Guid id)
    {
        var registration = await _registrationService.GetRegistrationById(id);
        registration.Status = RegistrationStatus.Canceled;
        var result=await _registrationService.UpdateRegistration(registration);

        if (result != null)
        {
            return Ok();
        }

        return StatusCode(500);
    }
}