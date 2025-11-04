using Microsoft.EntityFrameworkCore;
using Rdw.Repository.Entities;

namespace Rdw.Repository.Database;

public class VehicleRepository(RdwDbContext context) : IVehicleRepository
{
    public async Task AddAsync(VehicleEntity entity, CancellationToken token = default)
    {
        context.Vehicles.Add(entity);
        await context.SaveChangesAsync(token);
    }

    public async Task<bool> ExistsAsync(string licensePlate, CancellationToken token = default)
    {
        return await context.Vehicles.AnyAsync(v => v.LicensePlate.Equals(licensePlate, StringComparison.CurrentCultureIgnoreCase), token);
    }

    public async Task<VehicleEntity?> GetByLicensePlateAsync(string licensePlate, CancellationToken token = default)
    {
        return await context.Vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate, token);
    }
}
