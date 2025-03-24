using System.Xml.Linq;

namespace CarbonCalculator.Api.Models.AutoDataNet;

public class AutoDataVehicle : IVehicle
{
    public double CalculateCarbonEmission(double distanceInKm)
    {
        throw new NotImplementedException();
    }

    public List<AutoDataModel> ExtractXmlData()
    {
        string xmlFilePath = "C:\\Users\\chris\\Downloads\\auto-data.xml";
        XDocument document = XDocument.Load(xmlFilePath);

        XNamespace xmlns = "http://www.auto-data.net";

        var modifications = document.Descendants(xmlns + "modification")
            .Select(mod => new AutoDataModel
            {
                Brand = mod.Element(xmlns + "brand")?.Value ?? string.Empty,
                Model = mod.Element(xmlns + "model")?.Value ?? string.Empty,
                Generation = mod.Element(xmlns + "generation")?.Value ?? string.Empty,
                Engine = mod.Element(xmlns + "engine")?.Value ?? string.Empty,
                Fuel = mod.Element(xmlns + "fuel")?.Value ?? string.Empty,
                FuelConsumptionCombined = (double?)mod.Element(xmlns + "fuelConsumptionCombined") ?? 0.0,
            })
            .Where(mod => mod.FuelConsumptionCombined > 0)
            .ToList();

        return modifications;
    }
}
