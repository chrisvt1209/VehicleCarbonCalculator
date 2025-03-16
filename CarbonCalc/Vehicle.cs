using System.Text.Json.Serialization;

namespace CarbonCalc;

public record Vehicle
{
    [JsonPropertyName("kenteken")]
    public string LicensePlate { get; init; } = string.Empty;

    [JsonPropertyName("brandstof_omschrijving")]
    public string FuelType { get; init; } = string.Empty;

    [JsonPropertyName("co2_uitstoot_gecombineerd")]
    public string CarbonEmission { get; init; } = string.Empty;
}
