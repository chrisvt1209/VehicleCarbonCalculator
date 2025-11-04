using Rdw.Repository.Entities;

namespace Rdw.Repository.Database;

public interface IVehicleRepository
{
    Task AddAsync(VehicleEntity entity, CancellationToken token = default);
    Task<bool> ExistsAsync(string licensePlate, CancellationToken token = default);
    Task<VehicleEntity?> GetByLicensePlateAsync(string licensePlate, CancellationToken token = default);
}
