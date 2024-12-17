using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using Newtonsoft.Json;
using NLog;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MotorcycleMaintenanceSchedule.Application.Services.External.NotificationSchedule;

public class NotificationScheduleConsumer : BackgroundService
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    private readonly IConnection _connection;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly string _consumerQueueName;
    private IModel? _channel;

    public NotificationScheduleConsumer(
        IConnection connection,
        IServiceScopeFactory serviceScopeFactory,
        string consumerQueueName)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        _consumerQueueName = consumerQueueName ?? throw new ArgumentNullException(nameof(consumerQueueName));
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetry(
                retryCount: 5,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                onRetry: (exception, timespan, attempt, context) =>
                {
                    Logger.Error("RabbitMQ", $"{_connection.Endpoint}");
                    Logger.Error("RabbitMQ", $"Retry {attempt}: Connection to RabbitMQ failed. Exception: {exception.Message}. Next attempt in {timespan}.");
                });

        retryPolicy.Execute(() =>
        {
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _consumerQueueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            Logger.Debug(_consumerQueueName, $"Queue {_consumerQueueName} declared successfully.");
        });

        StartConsumer(stoppingToken);

        return Task.CompletedTask;
    }

    private void StartConsumer(CancellationToken stoppingToken)
    {
        if (_channel == null)
        {
            throw new InvalidOperationException("Channel is not initialized.");
        }

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var notification = JsonConvert.DeserializeObject<NotificationScheduleEntity>(message)!;

            Logger.Debug($"Message received from queue {_consumerQueueName}: {notification.Id}");

            using var scope = _serviceScopeFactory.CreateScope();

            var notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationScheduleRepository>();
            try
            {
                await notificationRepository.Create(notification);

                Logger.Debug("Message processed and saved to database successfully.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error saving message to database: {ex.Message}");
            }
        };

        _channel.BasicConsume(queue: _consumerQueueName,
                              autoAck: true,
                              consumer: consumer);

        Logger.Debug($"Consumer started on queue {_consumerQueueName}.");
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}
