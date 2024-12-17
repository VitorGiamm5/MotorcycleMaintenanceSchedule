
namespace MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

public sealed class ActionResult
{
    private object? Data { get; set; } = null;

    private PaginationMetadata? Pagination { get; set; }

    private List<ErrorDetails>? Error { get; set; } = [];

    public class ErrorDetails
    {
        public string? Message { get; set; }
        public object? Details { get; set; }
    }

    public bool HasError()
    {
        return Error?.Count != 0;
    }

    public bool HasData()
    {
        return Data != null;
    }

    public void SetError(object message, object? details = null)
    {
        Error?.Add(new ErrorDetails
        {
            Message = message?.ToString(),
            Details = details
        });
    }

    public void SetData(object data)
    {
        Data = data;
    }

    public void SetPaginationMetadata(PaginationMetadata pagination)
    {
        Pagination = pagination;
    }

    public void SetPaginationMetadata(int currentPage, int pageSize, int totalRecords)
    {
        Pagination = new PaginationMetadata(currentPage, pageSize, totalRecords);
    }

    public object GetData()
    {
        return new
        {
            Data,
            Pagination
        };
    }

    public object? GetError()
    {
        return new
        {
            Error
        };
    }
}
