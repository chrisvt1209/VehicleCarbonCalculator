using CarbonCalculator.Api.Converters;
using CarbonCalculator.Api.Models.Rdw;
using System.Text.Json;

namespace CarbonCalculator.Api.Util;

public class RdwClient
{
    private readonly HttpClient _httpClient = new();

    private const string BaseUrl = "https://opendata.rdw.nl/resource/";

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters =
        {
            new DoubleWithCommaConverter(),
            new IntegerInStringConverter(),
            new BooleanConverter(),
            new NullableStringConverter()
        }
    };

    public async Task<RdwVehicle?> GetVehicleAsync(string licensePlate)
    {
        return (await GetAsync<RdwVehicle>($"m9d7-ebf2.json?kenteken={licensePlate.Replace("-", "")}"))?.WithClient(this);
    }

    public RdwVehicle GetVehicle(string licensePlate) =>
        GetVehicleAsync(licensePlate).GetAwaiter().GetResult() ?? throw new InvalidOperationException();

    public async Task<RdwFuelInfo?> GetFuelInfoAsync(string licensePlate)
    {
        return (await GetAsync<RdwFuelInfo>($"8ys7-d773.json?kenteken={licensePlate.Replace("-", "")}"));
    }

    public RdwFuelInfo GetFuelInfo(string licensePlate) =>
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
