using CarbonCalculator.Api.Models.AutoDataNet;
using Microsoft.AspNetCore.Mvc;

namespace CarbonCalculator.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutoDataController : ControllerBase
    {
        private readonly AutoDataVehicle _autoDataVehicle;

        public AutoDataController()
        {
            _autoDataVehicle = new AutoDataVehicle();
        }

        [HttpGet("extract-xml-data")]
        public IActionResult ExtractXmlData()
        {
            var data = _autoDataVehicle.ExtractXmlData();
            return Ok(data);
        }
    }
}
