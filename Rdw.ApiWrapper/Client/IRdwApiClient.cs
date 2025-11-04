using Rdw.ApiWrapper.Models;

namespace Rdw.ApiWrapper.Client;

public interface IRdwApiClient
{
    Task<Vehicle?> GetVehicleAsync(string licensePlate, CancellationToken cancellationToken = default);
    Task<FuelInfo?> GetFuelInfoAsync(string licensePlate, CancellationToken cancellationToken = default);
}
