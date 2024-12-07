using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MotorcycleMaintenanceSchedule.Domain;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule.BaseRepositories;
using MotorcycleMaintenanceSchedule.Infrastructure.Database;
using MotorcycleMaintenanceSchedule.Infrastructure.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Infrastructure.Repositories.Schedule.BaseRepositories;
using Polly;
using Polly.Retry;

namespace MotorcycleMaintenanceSchedule.Infrastructure;

public static class SetupInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomain(configuration);

        AddRepositories(services);

        RetryPolicy retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetry(
                retryCount: 5,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (exception, timespan, attempt, context) =>
                {
                    Console.WriteLine($"Retry {attempt} fail with error: {exception.Message}. Lets try again {timespan}.");
                }
            );

        services.AddDbContextPool<ApplicationDbContext>(options =>
        {
            retryPolicy.Execute(() =>
            {
                options.UseNpgsql().AddInfrastructure(configuration);
            });
        });

        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.TryAddScoped<IScheduleListRepository, ScheduleListRepository>();
        services.TryAddScoped<IScheduleRepository, ScheduleRepository>();

        services.TryAddScoped<IBaseRepository<ScheduleEntity>, ScheduleListRepository>();
    }
}
