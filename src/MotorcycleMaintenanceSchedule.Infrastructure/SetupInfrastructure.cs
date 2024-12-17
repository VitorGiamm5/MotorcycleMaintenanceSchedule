using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MotorcycleMaintenanceSchedule.Domain;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule.BaseRepositories;
using MotorcycleMaintenanceSchedule.Domain.Settings.RabbitMQ;
using MotorcycleMaintenanceSchedule.Infrastructure.Database;
using MotorcycleMaintenanceSchedule.Infrastructure.Repositories.Schedule;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;

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

        services.Configure<RabbitMQSettings>(options => configuration.GetSection("RabbitMQ").Bind(options));

        services.TryAddSingleton<IConnectionFactory>(sp =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQ:Host"],
                UserName = configuration["RabbitMQ:Username"],
                Password = configuration["RabbitMQ:Password"],
                Port = int.Parse(configuration["RabbitMQ:Port"]!)
            };

            return factory;
        });

        services.AddSingleton<IConnection>(sp =>
        {
            var factory = sp.GetRequiredService<IConnectionFactory>();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry {retryCount} for RabbitMQ connection: {exception.Message}");

                    Console.WriteLine(
                        $"HostName: '{configuration["RabbitMQ:Host"]}'," +
                        $"UserName: '{configuration["RabbitMQ:Username"]}'," +
                        $"Password: '{configuration["RabbitMQ:Password"]}'," +
                        $"Port: '{configuration["RabbitMQ:Port"]}'");

                    Console.WriteLine(
                        $"Publish: '{configuration["RabbitMQ:QueuesName:MaintenanceSchedulePublishQueue"]}'," +
                        $"Consumer: '{configuration["RabbitMQ:QueuesName:MaintenanceScheduleConsumerQueue"]}'");

                });

            return policy.Execute(() => factory.CreateConnection());
        });

        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.TryAddScoped<IScheduleListRepository, ScheduleListRepository>();
        services.TryAddScoped<IScheduleRepository, ScheduleRepository>();
        services.TryAddScoped<INotificationScheduleRepository, NotificationScheduleRepository>();

        services.TryAddScoped<IBaseRepository<ScheduleEntity>, ScheduleListRepository>();
    }
}
