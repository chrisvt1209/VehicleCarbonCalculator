namespace Rdw.Service.Utils;

public class FuelTypeConfig(IConfiguration configuration) : IFuelTypeConfig
{
    private readonly IConfiguration configuration = configuration;

    public double GetEmissionKgPerLiter(string fuelType)
    {
        var emissionKgPerLiter = configuration
            .GetSection(FuelUtils.FuelTypes)
            .GetSection(fuelType)
            .GetValue<double>(FuelUtils.EmissionKgL);

        return emissionKgPerLiter;
    }
}
