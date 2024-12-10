using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace MotorcycleMaintenanceSchedule.Application.Services.External.NotificationSchedule;

public class NotificationScheduleConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly string _consumerQueueName;
    private IModel? _channel;

    public NotificationScheduleConsumer(
        IConnection connection,
        IServiceScopeFactory serviceScopeFactory,
        IConfiguration configuration)
    {
        _connection = connection;
        _serviceScopeFactory = serviceScopeFactory;
        _consumerQueueName = configuration["RabbitMQ:QueuesName:MaintenanceScheduleConsumerQueue"]
            ?? throw new BrokerUnreachableException(new Exception("RabbitMQ queue name is not configured."));
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
                    Console.WriteLine($"{_connection.Endpoint}");
                    Console.WriteLine($"Retry {attempt}: Connection to RabbitMQ failed. Exception: {exception.Message}. Next attempt in {timespan}.");
                });

        retryPolicy.Execute(() =>
        {
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _consumerQueueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            Console.WriteLine($"Queue {_consumerQueueName} declared successfully.");
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

            Console.WriteLine($"Message received from queue {_consumerQueueName}: {message}");

            var notification = JsonConvert.DeserializeObject<NotificationScheduleEntity>(message)!;

            using var scope = _serviceScopeFactory.CreateScope();

            var notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationScheduleRepository>();
            try
            {
                await notificationRepository.Create(notification);
                Console.WriteLine("Message processed and saved to database successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving message to database: {ex.Message}");
            }
        };

        _channel.BasicConsume(queue: _consumerQueueName,
                              autoAck: true,
                              consumer: consumer);

        Console.WriteLine($"Consumer started on queue {_consumerQueueName}.");
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}
