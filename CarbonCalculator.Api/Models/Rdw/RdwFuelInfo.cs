using System.Text.Json.Serialization;

namespace CarbonCalculator.Api.Models.Rdw;

public class RdwFuelInfo
{
    [JsonPropertyName("brandstof_volgnummer")]
    public int FuelTypeId { get; init; }

    [JsonPropertyName("brandstof_omschrijving")]
    public string FuelType { get; init; } = string.Empty;

    [JsonPropertyName("brandstofverbruik_gecombineerd")]
    public double AverageFuelConsumptionLight { get; init; }

    [JsonPropertyName("brandstof_verbruik_gecombineerd_wltp")]
    public double AverageFuelConsumptionHeavy { get; init; }

    [JsonPropertyName("elektrisch_verbruik_enkel_elektrisch_wltp")]
    public double AverageElectricConsumptionWltp { get; init; }

    [JsonPropertyName("actie_radius_enkel_elektrisch_wltp")]
    public double AverageActionRadiusWltp { get; init; }

    [JsonPropertyName("co2_uitstoot_gecombineerd")]
    public double AverageCarbonEmissionLight { get; init; }

    [JsonPropertyName("emissie_co2_gecombineerd_wltp")]
    public double AverageCarbonEmissionHeavy { get; init; }

    [JsonPropertyName("emissiecode_omschrijving")]
    public string EmissionCodeDescription { get; init; } = string.Empty;
}
