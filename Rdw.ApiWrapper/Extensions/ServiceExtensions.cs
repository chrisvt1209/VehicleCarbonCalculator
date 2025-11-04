using Microsoft.Extensions.DependencyInjection;
using Rdw.ApiWrapper.Client;

namespace Rdw.ApiWrapper.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddRdwApiWrapper(this IServiceCollection services)
    {
        services.AddScoped<IRdwApiClient, RdwApiClient>();

        return services;
    }
}
