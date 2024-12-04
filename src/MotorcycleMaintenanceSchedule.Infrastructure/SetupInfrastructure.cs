using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotorcycleMaintenanceSchedule.Domain;

namespace MotorcycleMaintenanceSchedule.Infrastructure;

public static class SetupInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomain(configuration);

        return services;
    }
}