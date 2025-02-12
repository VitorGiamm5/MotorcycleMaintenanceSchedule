namespace MotorcycleMaintenanceSchedule.Domain.Cache.Schedule;

public class ScheduleSummaryDto
{
    public required string MotorcycleId { get; set; }
    public required decimal Price { get; set; }
    public required string Date { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd");
}
