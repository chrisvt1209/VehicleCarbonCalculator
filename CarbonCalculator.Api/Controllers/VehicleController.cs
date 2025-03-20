using CarbonCalculator.Api.Util;
using Microsoft.AspNetCore.Mvc;

namespace CarbonCalculator.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
    private readonly RdwClient _rdwClient;

    public VehicleController(RdwClient rdwClient)
    {
        _rdwClient = rdwClient;
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

    [HttpGet("{licensePlate}/fuel-info")]
    public async Task<IActionResult> GetFuelInfo(string licensePlate)
    {
        var fuelInfo = await _rdwClient.GetFuelInfoAsync(licensePlate);

        if (fuelInfo is null)
        {
            return NotFound();
        }

        return Ok(fuelInfo);
    }

    [HttpGet("{licensePlate}/carbon-emission")]
    public async Task<IActionResult> GetCarbonEmission(string licensePlate, [FromQuery] double distance)
    {
        var vehicle = await _rdwClient.GetVehicleAsync(licensePlate);
        if (vehicle is null)
        {
            return NotFound();
        }

        var carbonEmission = vehicle.CalculateCarbonEmission(distance);

        return Ok(new
        {
            LicensePlate = licensePlate,
            DistanceInKm = distance,
            CarbonEmissionInKg = carbonEmission
        });
    }
}
