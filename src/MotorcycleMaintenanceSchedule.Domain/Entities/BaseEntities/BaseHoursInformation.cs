namespace MotorcycleMaintenanceSchedule.Domain.Entities.BaseEntities;

public class BaseHoursInformation : BaseAuditedEntity
{
    public DateTimeOffset? StartBusinessHour { get; set; }
    public DateTimeOffset? EndBusinessHour { get; set; }
    public DateTimeOffset? SchedulingDate { get; set; }

    public bool IsSchedulingDateValid()
    {
        var schedulingTime = SchedulingDate?.LocalDateTime.TimeOfDay;
        var startTime = StartBusinessHour?.LocalDateTime.TimeOfDay;
        var endTime = EndBusinessHour?.LocalDateTime.TimeOfDay;

        return schedulingTime >= startTime && schedulingTime <= endTime;
    }
}
