using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.NotificationSchedule;

public class NotificationSchedulePublisher : INotificationSchedulePublisher
{
    private readonly IConnection _connection;
    private readonly string _publisherQueueName;

    public NotificationSchedulePublisher(IConnection? connection, string? publisherQueueName)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _publisherQueueName = publisherQueueName ?? throw new ArgumentNullException(nameof(publisherQueueName));
    }

    public void PublishMotorcycle(NotificationScheduleEntity motorcycle)
    {
        ArgumentNullException.ThrowIfNull(motorcycle);

        try
        {
            using var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: _publisherQueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var message = JsonSerializer.Serialize(motorcycle);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: _publisherQueueName,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($"[x] Published message to {_publisherQueueName}: {message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error publishing message to {_publisherQueueName}: {ex.Message}");
            throw;
        }
    }
}

