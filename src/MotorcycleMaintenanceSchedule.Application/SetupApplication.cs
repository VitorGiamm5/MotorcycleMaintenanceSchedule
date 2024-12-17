using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotorcycleMaintenanceSchedule.Application.Services.External.NotificationSchedule;
using MotorcycleMaintenanceSchedule.Application.Services.Internal.NotificationSchedule;
using MotorcycleMaintenanceSchedule.Infrastructure;
using RabbitMQ.Client;
using System.Reflection;

namespace MotorcycleMaintenanceSchedule.Application;

public static class SetupApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddTransient<INotificationSchedulePublisher, NotificationSchedulePublisher>(sp =>
            new NotificationSchedulePublisher(
                sp.GetRequiredService<IConnection>(),
                publisherQueueName: configuration["RabbitMQ:QueuesName:MaintenanceSchedulePublishQueue"]!
            )
        );

        services.AddHostedService<NotificationScheduleConsumer>(sp =>
            new NotificationScheduleConsumer(
                sp.GetRequiredService<IConnection>(),
                sp.GetRequiredService<IServiceScopeFactory>(),
                consumerQueueName: configuration["RabbitMQ:QueuesName:MaintenanceScheduleConsumerQueue"]!
            )
        );

        return services;
    }
}
