using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using MotorcycleMaintenanceSchedule.Domain.Services.Internal.Schedule.Queries.List;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.List;

public class ScheduleListParamsQuery : ScheduleListParams, IRequest<ActionResult>
{
    public ScheduleListParamsQuery(ScheduleListParams request)
    {
        #region BaseQueryParams

        PageNumber = request.PageNumber;
        PageSize = request.PageSize;
        Ascending = request.Ascending;

        #endregion

        OrderBy = request.OrderBy;
        Status = request.Status;
        SearchField = request.SearchField;
        SearchValue = request.SearchValue;
    }
}
