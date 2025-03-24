namespace CarbonCalculator.Api.Models.AutoDataNet;

public class AutoDataModel
{
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Generation { get; set; } = string.Empty;
    public string Engine { get; set; } = string.Empty;
    public string Fuel { get; set; } = string.Empty;
    public double FuelConsumptionCombined { get; set; }
}
