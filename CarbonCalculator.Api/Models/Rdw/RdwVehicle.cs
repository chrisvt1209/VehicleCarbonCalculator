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

    [JsonPropertyName("brandstof_info")]
    public RdwFuelInfo FuelInfo =>
        (_fuelInfo ??= _client.GetFuelInfoAsync(LicensePlate).GetAwaiter().GetResult())
        ?? throw new InvalidOperationException();

    private RdwFuelInfo? _fuelInfo;

    public double CalculateCarbonEmission(double distanceInKm)
    {
        double totalCarbonEmissionInKg = 0;

        if (FuelInfo.FuelType.Contains("Elektriciteit"))
        {
            totalCarbonEmissionInKg = CalculateElectricVehicleEmissions(distanceInKm);
        }
        else if (FuelInfo.FuelType.Contains("Hybride"))
        {
            totalCarbonEmissionInKg = CalculateHybridVehicleEmissions(distanceInKm);
        }
        else
        {
            totalCarbonEmissionInKg = distanceInKm / ConvertToKilometersPerLiter() * ReturnEmissionPerLiter();
        }

        return totalCarbonEmissionInKg;
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

    private double ReturnEmissionPerLiter()
    {
        double emissionPerLiter = ConvertEmissionInGramToKilogram() * ConvertToKilometersPerLiter();
        return emissionPerLiter;
    }

    private double CalculateHybridVehicleEmissions(double distanceInKm)
    {
        double fuelEmission = distanceInKm / ConvertToKilometersPerLiter() * ReturnEmissionPerLiter();
        double electricEmission = CalculateElectricVehicleEmissions(distanceInKm);
        return fuelEmission + electricEmission;
    }

    private double CalculateElectricVehicleEmissions(double distanceInKm)
    {
        const double emissionFactorPerKwH = 0.25;
        double averageElectricConsumptionInKwH = FuelInfo.AverageElectricConsumptionWltp / 1000;
        double totalElectricityConsumption = distanceInKm * averageElectricConsumptionInKwH;
        double carbonEmissionInKg = totalElectricityConsumption * emissionFactorPerKwH;
        return carbonEmissionInKg;
    }
}
