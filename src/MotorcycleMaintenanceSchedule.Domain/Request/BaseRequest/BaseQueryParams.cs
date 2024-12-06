namespace MotorcycleMaintenanceSchedule.Domain.Request.BaseRequest;

public class BaseQueryParams
{
    public int? PageNumber { get; set; } = 1;
    public int? PageSize { get; set; } = 20;
    public bool? Ascending { get; set; } = false;

    public DateTime? CreationDateStart { get; set; }
    public DateTime? CreationDateEnd { get; set; }
}
