using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rdw.Repository.Database;

namespace Rdw.Repository.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IVehicleRepository, VehicleRepository>()
            .AddDbContext<RdwDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Database")));

        return services;
    }

    public static IServiceProvider ApplyMigration(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<RdwDbContext>();
        context.Database.Migrate();

        return serviceProvider;
    }
}
