using MotorcycleMaintenanceSchedule.Domain.Services.Internal.Schedule.Queries.List;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.List;

public static class ScheduleListMappers
{
    public static ScheduleListParams Map(ScheduleListParamsQuery request)
    {
        var result = new ScheduleListParams
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Ascending = request.Ascending,
            OrderBy = request.OrderBy,
            Status = request.Status,
            SearchField = request.SearchField,
            SearchValue = request.SearchValue
        };

        return result;
    }
}
