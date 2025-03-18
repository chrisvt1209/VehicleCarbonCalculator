using System.Text.Json.Serialization;

namespace CarbonCalculator.Api.Models;

public class FuelInfo
{
    [JsonPropertyName("brandstof_volgnummer")]
    public int FuelTypeId { get; init; }

    [JsonPropertyName("brandstof_omschrijving")]
    public string FuelType { get; init; } = string.Empty;

    [JsonPropertyName("brandstofverbruik_gecombineerd")]
    public float AverageFuelConsumption { get; init; }

    [JsonPropertyName("co2_uitstoot_gecombineerd")]
    public int AverageCarbonEmissionLight { get; init; }

    [JsonPropertyName("emissie_co2_gecombineerd_wltp")]
    public int AverageCarbonEmissionHeavy { get; init; }

    [JsonPropertyName("emissiecode_omschrijving")]
    public string EmissionCodeDescription { get; init; } = string.Empty;
}
