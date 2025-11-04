using Rdw.ApiWrapper.Converters;
using Rdw.ApiWrapper.Models;
using System.Text.Json;

namespace Rdw.ApiWrapper.Client;

public class RdwApiClient : IRdwApiClient
{
    private readonly HttpClient httpClient = new();
    private const string BaseUrl = "https://opendata.rdw.nl/resource/";

    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        Converters =
        {
            new BoolConverter(),
            new DateConverter(),
            new DoubleWithCommaConverter(),
            new IntegerInStringConverter(),
            new NullableStringConverter()
        },
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
    };

    public async Task<Vehicle?> GetVehicleAsync(string licensePlate, CancellationToken cancellationToken = default) =>
        (await GetAsync<Vehicle>($"m9d7-ebf2.json?kenteken={licensePlate.Replace("-", "")}"))?.WithClient(this);

    public async Task<FuelInfo?> GetFuelInfoAsync(string licensePlate, CancellationToken cancellationToken = default) =>
        (await GetAsync<FuelInfo>($"8ys7-d773.json?kenteken={licensePlate.Replace("-", "")}"));

    protected async Task<T?> GetAsync<T>(string endpoint) where T : class
    {
        var response = await httpClient.GetAsync($"{BaseUrl}{endpoint}");

        if (response is null || !response.IsSuccessStatusCode)
        {
            return null;
        }

        return JsonSerializer.Deserialize<IEnumerable<T>>(await response.Content.ReadAsStringAsync(), jsonSerializerOptions)?.FirstOrDefault();
    }
}
