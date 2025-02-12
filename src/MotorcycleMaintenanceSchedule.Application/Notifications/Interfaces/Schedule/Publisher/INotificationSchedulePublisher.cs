using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

namespace MotorcycleMaintenanceSchedule.Application.Notifications.Interfaces.Schedule.Publisher;

public interface INotificationSchedulePublisher
{
    void PublishMotorcycle(NotificationScheduleEntity motorcycle);
}
