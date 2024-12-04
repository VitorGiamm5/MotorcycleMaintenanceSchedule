namespace MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

public class PaginationMetadata
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }

    public PaginationMetadata(int currentPage, int pageSize, int totalRecords)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
    }
}