namespace MotorcycleMaintenanceSchedule.Domain.Cache.Schedule;

public class DailySummaryDto
{
    public required string Date { get; set; }
    public decimal TotalPrice { get; set; }
    public int TotalMotorcycles { get; set; }
}