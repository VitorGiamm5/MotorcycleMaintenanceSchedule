using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.Summary;

public static class ScheduleSummaryMappers
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
            DateCreated = DateTime.UtcNow
        };

        return result;
    }
}
