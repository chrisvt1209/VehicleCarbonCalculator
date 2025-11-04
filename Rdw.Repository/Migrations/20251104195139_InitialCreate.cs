using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rdw.Repository.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Vehicles",
            columns: table => new
            {
                LicensePlate = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Year = table.Column<int>(type: "int", nullable: true),
                FuelType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FuelConsumptionLiters = table.Column<double>(type: "float", nullable: true),
                CarbonEmissionGramKm = table.Column<double>(type: "float", nullable: true),
                ElectricConsumptionWh = table.Column<double>(type: "float", nullable: true),
                MeasureMethod = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Vehicles", x => x.LicensePlate);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Vehicles_LicensePlate",
            table: "Vehicles",
            column: "LicensePlate",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Vehicles");
    }
}
