using Rdw.Service.Models;

namespace Rdw.Service.Results;

public class VehicleEmissionResult
{
    public VehicleModel? Vehicle { get; set; }
    public double? TotalEmissionKg { get; set; }
}
