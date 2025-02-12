using MotorcycleMaintenanceSchedule.Domain.Cache.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Create;

public static class ScheduleCreateMappers
{
    public static NotificationScheduleEntity ToNotification(ScheduleEntity data)
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

    public static ScheduleEntity ToEntity(ScheduleCreateCommand data)
    {
        var result = new ScheduleEntity()
        {
            Id = Ulid.NewUlid().ToString(),
            Name = data.Name,
            Email = data.Email,
            Phone = data.Phone,
            PhoneDDD = data.PhoneDDD,
            Price = data.Price,
            Observation = data.Observation,
            Status = data.Status,
            MotorcycleId = data.MotorcycleId,
            StartBusinessHour = data.StartBusinessHour,
            EndBusinessHour = data.EndBusinessHour,
            ScheduleDate = data.ScheduleDate,
            CreatedBy = "admin",
            DateCreated = DateTime.UtcNow
        };

        return result;
    }

    public static ScheduleSummaryDto ToSummary(ScheduleEntity data)
    {
        var result = new ScheduleSummaryDto()
        {
            MotorcycleId = data.MotorcycleId!,
            Price = data.Price ?? 0,
            Date = DateTime.UtcNow.ToString("yyyy-MM-dd")
        };

        return result;
    }
}
