using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.NotificationSchedule;

public interface INotificationSchedulePublisher
{
    void PublishMotorcycle(NotificationScheduleEntity motorcycle);
}
