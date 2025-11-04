using System.Text.Json.Serialization;

namespace Rdw.ApiWrapper.Models;

public class FuelInfo
{
    [JsonPropertyName("brandstof_volgnummer")]
    public int? FuelTypeId { get; set; }

    [JsonPropertyName("brandstof_omschrijving")]
    public string? FuelType { get; set; }

    #region fuel consumption

    [JsonPropertyName("brandstofverbruik_gecombineerd")]
    public double? FuelConsumptionLitersNedc { get; set; }

    [JsonPropertyName("brandstof_verbruik_gecombineerd_wltp")]
    public double? FuelConsumptionLitersWltp { get; set; }

    [JsonPropertyName("brandstof_verbruik_gewogen_wltp")]
    public double? FuelConsumptionLitersWeightedWltp { get; set; }

    [JsonPropertyName("brandstof_verbruik_gewogen_gecombineerd_wltp")]
    public double? FuelConsumptionLitersWeightedCombinedWltp { get; set; }

    #endregion

    #region carbon emissions

    [JsonPropertyName("co2_uitstoot_gecombineerd")]
    public double? CarbonEmissionGramNedc { get; set; }

    [JsonPropertyName("co2_uitstoot_gewogen")]
    public double? CarbonEmissionGramWeighted { get; set; }

    [JsonPropertyName("emissie_co2_gecombineerd_wltp")]
    public double? CarbonEmissionGramWltp { get; set; }

    [JsonPropertyName("emis_co2_gewogen_gecombineerd_wltp")]
    public double? CarbonEmissionGramWeightedWltp { get; set; }

    #endregion

    #region electric consumption

    [JsonPropertyName("elektrisch_verbruik_enkel_elektrisch_wltp")]
    public double? ElectricConsumptionWhSingleWltp { get; set; }

    [JsonPropertyName("elektrisch_verbruik_extern_opladen_wltp")]
    public double? ElectricConsumptionWhExternChargingWltp { get; set; }

    #endregion
}
