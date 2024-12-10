using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Create;

public static class ScheduleToNotificationMappers
{
    public static NotificationScheduleEntity Map(ScheduleEntity data)
    {
        var result = new NotificationScheduleEntity()
        {
            Id = Ulid.NewUlid().ToString(),
            ScheduleId = data.Id!,
            Name = data.Name,
            MotorcyleId = data.MotorcycleId,
            ScheduleDate = data.ScheduleDate.ToString(),
            Status = data.Status,
            NotificationDate = DateTime.UtcNow
        };

        return result;
    }
}
