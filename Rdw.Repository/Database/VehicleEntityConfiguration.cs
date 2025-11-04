using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rdw.Repository.Entities;

namespace Rdw.Repository.Database;

internal class VehicleEntityConfiguration : IEntityTypeConfiguration<VehicleEntity>
{
    public void Configure(EntityTypeBuilder<VehicleEntity> builder)
    {
        builder.HasKey(x => x.LicensePlate);
        builder.HasIndex(x => x.LicensePlate).IsUnique();
        builder.Property(x => x.LicensePlate).HasMaxLength(6);

        builder.Property(x => x.Brand);

        builder.Property(x => x.Model);

        builder.Property(x => x.Year);

        builder.Property(x => x.FuelType);

        builder.Property(x => x.FuelConsumptionLiters);

        builder.Property(x => x.CarbonEmissionGramKm);

        builder.Property(x => x.ElectricConsumptionWh);

        builder.Property(x => x.MeasureMethod)
            .HasConversion<string>();
    }
}
