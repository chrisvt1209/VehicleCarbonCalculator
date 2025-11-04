using Rdw.ApiWrapper.Client;
using System.Text.Json.Serialization;

namespace Rdw.ApiWrapper.Models;

public class Vehicle
{
    private RdwApiClient? client;

    internal Vehicle WithClient(RdwApiClient client)
    {
        this.client = client;
        return this;
    }

    [JsonPropertyName("kenteken")]
    public string? LicensePlate { get; set; }

    [JsonPropertyName("merk")]
    public string? Brand { get; set; }

    [JsonPropertyName("handelsbenaming")]
    public string? Model { get; set; }

    [JsonPropertyName("datum_eerste_toelating")]
    public DateOnly? RegistrationYear { get; set; }

    public int? ModelYear => RegistrationYear?.Year;

    public double? TotalEmissionKg { get; set; }

    public FuelInfo? FuelInfo
    {
        get => fuelInfo ??= client?.GetFuelInfoAsync(LicensePlate, default).GetAwaiter().GetResult();
        set => fuelInfo = value;
    }
    private FuelInfo? fuelInfo;
}
