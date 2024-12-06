namespace MotorcycleMaintenanceSchedule.Domain.Schedule;

public class ScheduleListParams
{
    public int Limit { get; set; }
    public int Page { get; set; }
    public ScheduleOrderByEnum OrderBy { get; set; }
    public bool Ascending { get; set; }
    public StatusMaintenanceEnum? Status { get; set; }
    public ScheduleSearchFieldEnum? SearchField { get; set; }
    public string? SearchValue { get; set; }
}
