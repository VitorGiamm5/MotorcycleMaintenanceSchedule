using MotorcycleMaintenanceSchedule.Domain.Entities.BaseEntities;
using MotorcycleMaintenanceSchedule.Domain.Services.Schedule.Queries.List;
using System.Text.Json.Serialization;

namespace MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

public class ScheduleEntity : BaseHoursInformation
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Id { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public int? Phone { get; set; }
    public int? PhoneDDD { get; set; }
    public string? Observation { get; set; }
    public ScheduleStatusEnum Status { get; set; }
    public string? MotorcycleId { get; set; }
    public decimal? Price { get; set; }
}
