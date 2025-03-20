using CarbonCalculator.Api.Util;
using System.Text.Json.Serialization;

namespace CarbonCalculator.Api.Models.Rdw;

public class RdwVehicle : IVehicle
{
    private RdwClient _client;

    internal RdwVehicle WithClient(RdwClient client)
    {
        _client = client;
        return this;
    }

    [JsonPropertyName("kenteken")]
    public string LicensePlate { get; init; } = string.Empty;

    [JsonPropertyName("voertuigsoort")]
    public string VehicleType { get; init; } = string.Empty;

    [JsonPropertyName("merk")]
    public string Brand { get; init; } = string.Empty;

    [JsonPropertyName("handelsbenaming")]
    public string Model { get; init; } = string.Empty;

    [JsonPropertyName("taxi_indicator")]
    public bool IsTaxi { get; init; }

    [JsonPropertyName("brandstof_info")]
    public RdwFuelInfo FuelInfo =>
        (_fuelInfo ??= _client.GetFuelInfoAsync(LicensePlate).GetAwaiter().GetResult())
        ?? throw new InvalidOperationException();

    private RdwFuelInfo? _fuelInfo;

    public double CalculateCarbonEmission(double distanceInKm)
    {
        double carbonEmissionInKilogram = 0;

        if (FuelInfo.FuelType.Contains("Elektriciteit"))
        {
            carbonEmissionInKilogram = CalculateElectricVehicleEmissions(distanceInKm);
        }
        else
        {
            carbonEmissionInKilogram = (distanceInKm / ConvertToKilometersPerLiter()) * ConvertEmissionInGramToKilogram();
        }            

        return carbonEmissionInKilogram;
    }

    private double ConvertToKilometersPerLiter()
    {

        if (FuelInfo.AverageFuelConsumptionHeavy > 0)
        {
            return 100 / FuelInfo.AverageFuelConsumptionHeavy;
        }

        return 100 / FuelInfo.AverageFuelConsumptionLight;
    }

    private double ConvertEmissionInGramToKilogram()
    {
        if (FuelInfo.AverageCarbonEmissionHeavy > 0)
        {
            return FuelInfo.AverageCarbonEmissionHeavy / 1000;
        }

        return FuelInfo.AverageCarbonEmissionLight / 1000;
    }

    private double CalculateElectricVehicleEmissions(double distanceInKm)
    {
        const double emissionFactorPerKwH = 0.22;

        double averageElectricConsumptionInKwH = FuelInfo.AverageElectricConsumptionWltp / 1000;

        double totalElectricityConsumption = (distanceInKm * averageElectricConsumptionInKwH) / 100;
        
        double carbonEmissionInKilogram = totalElectricityConsumption * emissionFactorPerKwH;

        return carbonEmissionInKilogram;
    }
}
