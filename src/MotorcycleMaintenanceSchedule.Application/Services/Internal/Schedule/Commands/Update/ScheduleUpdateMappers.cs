using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Update;

public static class ScheduleUpdateMappers
{
    public static ScheduleEntity Map(ScheduleUpdateCommand command)
    {
        var result = new ScheduleEntity()
        {
            Name = command.Name,
            Email = command.Email,
            Phone = command.Phone,
            PhoneDDD = command.PhoneDDD,
            Observation = command.Observation,
            Status = command.Status,
            MotorcycleId = command.MotorcycleId,
            StartBusinessHour = command.StartBusinessHour,
            EndBusinessHour = command.EndBusinessHour,
            DateLastUpdate = DateTime.UtcNow
        };

        return result;
    }
}
