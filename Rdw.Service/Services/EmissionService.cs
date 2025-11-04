using AutoMapper;
using Rdw.ApiWrapper.Client;
using Rdw.ApiWrapper.Models;
using Rdw.Repository.Database;
using Rdw.Repository.Entities;
using Rdw.Service.Models;
using Rdw.Service.Utils;

namespace Rdw.Service.Services;

public class EmissionService(IRdwApiClient client, IVehicleRepository repository,
    IFuelTypeConfig config, IMapper mapper)
{
    public async Task<double?> CalculateEmissionAsync(string licensePlate, double totalDistanceKm, CancellationToken token)
    {
        double? totalEmissionKg;

        var entity = await repository.GetByLicensePlateAsync(licensePlate, token);

        if (entity is not null)
        {
            totalEmissionKg = entity.CarbonEmissionGramKm * totalDistanceKm / 1000;
            return RoundDecimal(totalEmissionKg);
        }

        var vehicleApiModel = await client.GetVehicleAsync(licensePlate, token);
        if (vehicleApiModel is null)
            return null;

        var vehicleDomainModel = mapper.Map<VehicleModel>(vehicleApiModel);
        vehicleDomainModel.MeasureMethod = DetermineMeasureMethod(vehicleApiModel?.FuelInfo);

        var newEntity = mapper.Map<VehicleEntity>(vehicleDomainModel);
        await repository.AddAsync(newEntity, token);

        totalEmissionKg = CalculateTotalEmission(vehicleApiModel, totalDistanceKm);
        return RoundDecimal(totalEmissionKg);
    }

    private double? CalculateTotalEmission(Vehicle? vehicle, double totalDistanceKm)
    {
        var fuelInfo = vehicle?.FuelInfo;

        if (fuelInfo is null)
            return null;

        if (fuelInfo.FuelType == "Elektriciteit")
            return 0;

        if (HasValidEmissionValue(fuelInfo))
            return CalculateTotalEmissionWithEmissionValue(vehicle, totalDistanceKm);

        var emissionKgPerLiter = config.GetEmissionKgPerLiter(fuelInfo.FuelType);
        var kmPerLiter = ConvertToKmPerLiter(vehicle);

        if (kmPerLiter is null || emissionKgPerLiter == 0)
            return null;

        return (totalDistanceKm / kmPerLiter) * emissionKgPerLiter;
    }

    private static bool HasValidEmissionValue(FuelInfo fuelInfo)
    {
        return (fuelInfo.CarbonEmissionGramWltp ?? 0) > 0
            || (fuelInfo.CarbonEmissionGramNedc ?? 0) > 0;
    }

    private static double? CalculateTotalEmissionWithEmissionValue(Vehicle? vehicle, double totalDistanceKm)
    {
        if (vehicle?.FuelInfo?.CarbonEmissionGramWltp > 0)
            return totalDistanceKm * vehicle.FuelInfo?.CarbonEmissionGramWltp / 1000;

        if (vehicle?.FuelInfo?.CarbonEmissionGramNedc > 0)
            return totalDistanceKm * vehicle?.FuelInfo?.CarbonEmissionGramNedc / 1000;

        return 0;
    }

    private static double? ConvertToKmPerLiter(Vehicle? vehicle)
    {
        if (vehicle?.FuelInfo?.FuelConsumptionLitersWltp > 0)
            return 100 / vehicle.FuelInfo?.FuelConsumptionLitersWltp;

        if (vehicle?.FuelInfo?.FuelConsumptionLitersNedc > 0)
            return 100 / vehicle?.FuelInfo?.FuelConsumptionLitersNedc;

        return null;
    }

    private static MeasureMethodModel DetermineMeasureMethod(FuelInfo? fuelInfo) => fuelInfo switch
    {
        { CarbonEmissionGramWltp: > 0 }
        or { FuelConsumptionLitersWltp: > 0 }
        or { ElectricConsumptionWhSingleWltp: > 0 }
        => MeasureMethodModel.WLTP,

        { CarbonEmissionGramNedc: > 0 }
        or { FuelConsumptionLitersNedc: > 0 }
        => MeasureMethodModel.NEDC,

        _ => MeasureMethodModel.OTHER
    };

    private static double? RoundDecimal(double? value)
    {
        return value is null ? null : Math.Round(value.Value, 2, MidpointRounding.AwayFromZero);
    }
}
