using CarbonCalc;
using System.Text.Json;

Console.Write("Voer het kenteken in zonder spaties of streepjes: ");
string licensePlate = Console.ReadLine()?.ToUpper();

if (string.IsNullOrWhiteSpace(licensePlate))
{
    Console.WriteLine("Het kenteken mag niet leeg zijn.");
    return;
}

Console.Write("Voer het aantal gereden kilometers in: ");
if (!double.TryParse(Console.ReadLine(), out double kilometres) || kilometres <= 0)
{
    Console.WriteLine("Het aantal gereden kilometers moet een positief getal zijn.");
    return;
}

await GetVehicleEmissionAsync(licensePlate, kilometres);

static async Task GetVehicleEmissionAsync(string licensePlate, double kilometres)
{
    using HttpClient client = new();
    string url = $"https://opendata.rdw.nl/resource/8ys7-d773.json?kenteken={licensePlate}";

    try
    {
        var response = await client.GetStringAsync(url);
        var data = JsonSerializer.Deserialize<Vehicle[]>(response);

        if (data is not null && data.Length > 0 && data[0].CarbonEmission is not null &&
            double.TryParse(data[0].CarbonEmission, out double co2PerKm))
        {
            double totalEmission = (co2PerKm * kilometres) / 1000;
            Console.WriteLine($"De CO2-uitstoot voor voertuig {licensePlate} is {co2PerKm} g/km.");
            Console.WriteLine($"Voor een rit van {kilometres} km is de geschatte CO2-uitstoot: {totalEmission} kilogram.");
        }
        else
        {
            Console.WriteLine($"Geen CO2-uitstoot gegevens beschikbaar voor kenteken {licensePlate}.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Fout bij het ophalen van gegevens: {ex.Message}");
    }
}