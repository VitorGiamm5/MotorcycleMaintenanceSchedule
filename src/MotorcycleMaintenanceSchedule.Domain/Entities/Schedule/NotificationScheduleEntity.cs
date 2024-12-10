using MotorcycleMaintenanceSchedule.Domain.Services.Iternal.Schedule.Queries.List;

namespace MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

public class NotificationScheduleEntity
{
    public required string Id { get; set; }
    public required string ScheduleId { get; set; }
    public string? MotorcyleId { get; set; }
    public required string Name { get; set; }
    public string? ScheduleDate { get; set; }
    public ScheduleStatusEnum Status { get; set; }
    public required DateTime NotificationDate { get; set; }
}
