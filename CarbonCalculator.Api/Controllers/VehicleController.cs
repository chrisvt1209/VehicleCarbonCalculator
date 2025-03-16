using CarbonCalculator.Api.Util;
using Microsoft.AspNetCore.Mvc;

namespace CarbonCalculator.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
    private readonly RdwClient _rdwClient;

    public VehicleController()
    {
        _rdwClient = new RdwClient();
    }

    [HttpGet("{licensePlate}")]
    public async Task<IActionResult> GetVehicleData(string licensePlate)
    {
        var vehicle = await _rdwClient.GetVehicleAsync(licensePlate);

        if (vehicle is null)
        {
            return NotFound();
        }

        return Ok(vehicle);
    }
}
