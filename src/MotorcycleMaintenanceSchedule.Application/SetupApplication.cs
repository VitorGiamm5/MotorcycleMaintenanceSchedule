using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotorcycleMaintenanceSchedule.Infrastructure;

using System.Reflection;

namespace MotorcycleMaintenanceSchedule.Application;

public static class SetupApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}