namespace MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

public class PaginationMetadata
{
    public PaginationMetadata(int currentPage, int pageSize, int totalRecords)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
    }

    public int CurrentPage { get; protected set; }
    public int PageSize { get; protected set; }
    public int TotalRecords { get; protected set; }
    public int TotalPages { get; protected set; }
}