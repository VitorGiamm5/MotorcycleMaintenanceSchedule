using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotorcycleMaintenanceSchedule.Application.Cache.Interfaces;
using MotorcycleMaintenanceSchedule.Application.Cache.Schedule;
using MotorcycleMaintenanceSchedule.Application.Notifications.Interfaces.Schedule.Publisher;
using MotorcycleMaintenanceSchedule.Application.Notifications.Services.Schedule.RabitMQ.Consumer;
using MotorcycleMaintenanceSchedule.Application.Notifications.Services.Schedule.RabitMQ.Publisher;
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

        #region Cache

        services.AddScoped<IMotorcycleCacheService, MotorcycleCacheService>();

        #endregion

        #region Validators

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddFluentValidationAutoValidation();

        #endregion

        return services;
    }
}
