using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using MotorcycleMaintenanceSchedule.Domain.Services.Internal.Schedule.Queries.Summary;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.Summary;

public class ScheduleSummaryParamsQuery : ScheduleSummaryParams, IRequest<ActionResult>
{
    public ScheduleSummaryParamsQuery(ScheduleSummaryParams request)
    {
        #region BaseQueryParams

        PageNumber = request.PageNumber;
        PageSize = request.PageSize;
        Ascending = request.Ascending;

        #endregion
    }
}
