namespace MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

public class PaginatedResult<T> where T : class
{
    public List<T> Items { get; set; } = [];
    public PaginationMetadata? Pagination { get; set; }
}
