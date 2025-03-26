using System.Text.Json.Serialization;

namespace CarbonCalculator.Api.Models.AutoData;

public class AutoDataVehicle : IVehicle
{
    [JsonPropertyName("brand")]
    public string Brand { get; set; } = string.Empty;

    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [JsonPropertyName("modelYear")]
    public string ModelYear { get; set; } = string.Empty;

    [JsonPropertyName("fuel")]
    public string FuelType { get; set; } = string.Empty;

    [JsonPropertyName("fuelConsumptionUrbanMin")]
    public float FuelConsumptionCombinedMin { get; set; }

    [JsonPropertyName("fuelConsumptionUrbanMax")]
    public float FuelConsumptionCombinedMax { get; set; }

    [JsonPropertyName("co2Min")]
    public int Co2Min { get; set; }

    [JsonPropertyName("co2Max")]
    public int Co2Max { get; set; }

    [JsonPropertyName("allElectricRangeMin")]
    public float AllElectricRangeMin { get; set; }

    [JsonPropertyName("allElectricRangeMax")]
    public float AllElectricRangeMax { get; set; }

    public double CalculateCarbonEmission(double distanceInKm)
    {
        throw new NotImplementedException();
    }
}
