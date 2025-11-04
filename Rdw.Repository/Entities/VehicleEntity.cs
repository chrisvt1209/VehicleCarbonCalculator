namespace Rdw.Repository.Entities;

public class VehicleEntity
{
    public string LicensePlate { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public string? FuelType { get; set; }
    public double? FuelConsumptionLiters { get; set; }
    public double? CarbonEmissionGramKm { get; set; }
    public double? ElectricConsumptionWh { get; set; }
    public MeasureMethod MeasureMethod { get; set; }
}
