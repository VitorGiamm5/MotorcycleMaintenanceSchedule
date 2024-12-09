using MotorcycleMaintenanceSchedule.Domain.Entities.BaseEntities;
using MotorcycleMaintenanceSchedule.Domain.Services.Iternal.Schedule.Queries.List;
using System.Text.Json.Serialization;

namespace MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

public class ScheduleEntity : BaseHoursInformation
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public required string Id { get; set; } = "";
    public required string Name { get; set; } = "";
    public required string Email { get; set; } = "";
    public required int Phone { get; set; } = 0;
    public required int PhoneDDD { get; set; } = 0;
    public required string Observation { get; set; } = "";
    public ScheduleStatusEnum Status { get; set; }
    public required string MotorcycleId { get; set; } = "";
}
