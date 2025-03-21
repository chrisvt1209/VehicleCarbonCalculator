namespace CarbonCalculator.Api.Models;

public interface IVehicle
{
    double CalculateCarbonEmission(double distanceInKm);
}
