using MotorcycleMaintenanceSchedule.Domain.Request.BaseRequest;

namespace MotorcycleMaintenanceSchedule.Domain.Services.Internal.Schedule.Queries.List;

public class ScheduleListParams : BaseQueryParams
{
    public ScheduleOrderByEnum? OrderBy { get; set; }
    public ScheduleStatusEnum? Status { get; set; }
    public ScheduleSearchFieldEnum? SearchField { get; set; }
    public string? SearchValue { get; set; }

    public DateTime? ScheduleDateStart { get; set; }
    public DateTime? ScheduleDateEnd { get; set; }
}
