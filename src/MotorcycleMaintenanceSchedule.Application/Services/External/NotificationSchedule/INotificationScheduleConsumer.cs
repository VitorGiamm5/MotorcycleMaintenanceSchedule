namespace MotorcycleMaintenanceSchedule.Application.Services.External.NotificationSchedule;

public interface INotificationScheduleConsumer
{
    private Task StartAsync(CancellationToken cancellationToken);
    private Task StopAsync(CancellationToken cancellationToken);
}
