using MotorcycleMaintenanceSchedule.Domain.Entities.BaseEntities;
using MotorcycleMaintenanceSchedule.Domain.Schedule;

namespace MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

public class ScheduleEntity : BaseHoursInformation
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public int? Phone { get; set; }
    public int? PhoneDDD { get; set; }
    public string? Observation { get; set; }
    public StatusMaintenanceEnum Status { get; set; }
    public string? MotorcycleId { get; set; }
}
