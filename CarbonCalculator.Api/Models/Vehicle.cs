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
    public string LicensePlate { get; init; }

    [JsonPropertyName("voertuigsoort")]
    public string VehicleType { get; init; }

    [JsonPropertyName("merk")]
    public string Brand { get; init; }

    [JsonPropertyName("handelsbenaming")]
    public string Model { get; init; }

    [JsonPropertyName("taxi_indicator")]
    public bool IsTaxi { get; init; }

    public FuelInfo FuelInfo =>
        (_fuelInfo ??= _client.GetFuelInfoAsync(LicensePlate).GetAwaiter().GetResult())
        ?? throw new InvalidOperationException();

    private FuelInfo? _fuelInfo;
}
