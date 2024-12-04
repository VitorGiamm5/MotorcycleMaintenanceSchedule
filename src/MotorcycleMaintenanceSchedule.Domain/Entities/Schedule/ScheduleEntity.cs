using MotorcycleMaintenanceSchedule.Domain.Entities.BaseEntities;
using MotorcycleMaintenanceSchedule.Domain.Maintence;

namespace MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

public class ScheduleEntity : BaseHoursInformation
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required int Phone { get; set; }
    public required int PhoneDDD { get; set; }
    public string? Observation { get; set; }
    public StatusMaintenanceEnum Status { get; set; } = StatusMaintenanceEnum.AwaitingForSchedule;
}
