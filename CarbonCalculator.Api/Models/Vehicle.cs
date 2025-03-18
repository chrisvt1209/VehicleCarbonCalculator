using CarbonCalculator.Api.Util;
using System.Text.Json.Serialization;

namespace CarbonCalculator.Api.Models;

public class Vehicle
{
    private RdwClient _client;

    internal Vehicle WithClient(RdwClient client)
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

    public FuelInfo FuelInfo =>
        (_fuelInfo ??= _client.GetFuelInfoAsync(LicensePlate).GetAwaiter().GetResult())
        ?? throw new InvalidOperationException();

    private FuelInfo? _fuelInfo;

    public double CalculateCarbonEmission(double distance)
    {
        double carbonEmission = 0;
        if (FuelInfo.AverageCarbonEmissionHeavy > 0)
        {
            carbonEmission = FuelInfo.AverageCarbonEmissionHeavy;
        }

        return carbonEmission * distance;
    }
}
