using CarbonCalculator.Api.Converters;
using CarbonCalculator.Api.Models;
using System.Text.Json;

namespace CarbonCalculator.Api.Util;

public class RdwClient
{
    private readonly HttpClient _httpClient = new();
    const string BaseUrl = "https://opendata.rdw.nl/resource/";

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters =
        {
            new FloatInStringConverter(),
            new IntegerInStringConverter(),
            new RdwBooleanConverter(),
            new NullableStringConverter()
        }
    };

    public async Task<Vehicle?> GetVehicleAsync(string licensePlate)
    {
        return (await GetAsync<Vehicle>($"m9d7-ebf2.json?kenteken={licensePlate.Replace("-", "")}"));
    }

    public Vehicle GetVehicle(string licensePlate) =>
        GetVehicleAsync(licensePlate).GetAwaiter().GetResult() ?? throw new InvalidOperationException();

    public async Task<FuelInfo?> GetFuelInfoAsync(string licensePlate)
    {
        return (await GetAsync<FuelInfo>($"8ys7-d773.json?kenteken={licensePlate.Replace("-", "")}"));
    }

    public FuelInfo GetFuelInfo(string licensePlate) =>
        GetFuelInfoAsync(licensePlate).GetAwaiter().GetResult() ?? throw new InvalidOperationException();

    protected async Task<T?> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}{endpoint}");

        if (response is null || !response.IsSuccessStatusCode)
        {
            throw new NullReferenceException("Request failed");
        }

        return JsonSerializer.Deserialize<IEnumerable<T>>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions)!.FirstOrDefault();
    }
}
