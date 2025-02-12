using MotorcycleMaintenanceSchedule.Domain.Services.Internal.Schedule.Queries.List;

namespace MotorcycleMaintenanceSchedule.Domain.Cache.Schedule;

public class ScheduleSummaryDto
{
    public required string ScheduleId { get; set; }
    public required ScheduleStatusEnum Status { get; set; }
    public required decimal Price { get; set; }
    public required string Date { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd");
}
