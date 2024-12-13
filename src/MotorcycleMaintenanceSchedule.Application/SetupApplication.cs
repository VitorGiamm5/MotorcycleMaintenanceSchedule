using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotorcycleMaintenanceSchedule.Application.Services.External.NotificationSchedule;
using MotorcycleMaintenanceSchedule.Application.Services.Internal.NotificationSchedule;
using MotorcycleMaintenanceSchedule.Domain.Settings.RabbitMQ;
using MotorcycleMaintenanceSchedule.Infrastructure;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Polly;
using RabbitMQ.Client;
using System.Reflection;

namespace MotorcycleMaintenanceSchedule.Application;

public static class SetupApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

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
            var policy = Polly.Policy
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

        services.TryAddTransient<NotificationSchedulePublisher>();
        services.AddHostedService<NotificationScheduleConsumer>();

        return services;
    }
}