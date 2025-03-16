using System.Text.Json.Serialization;

namespace CarbonCalculator.Api.Models;

public class FuelInfo
{
    [JsonPropertyName("brandstof_volgnummer")]
    public int FuelTypeId { get; init; }

    [JsonPropertyName("brandstof_omschrijving")]
    public string FuelType { get; init; }

    [JsonPropertyName("brandstofverbruik_gecombineerd")]
    public float AverageFuelConsumption { get; init; }

    [JsonPropertyName("co2_uitstoot_gecombineerd")]
    public int AverageCarbonEmission { get; init; }

    [JsonPropertyName("emissiecode_omschrijving")]
    public string EmissionCodeDescription { get; init; }
}
