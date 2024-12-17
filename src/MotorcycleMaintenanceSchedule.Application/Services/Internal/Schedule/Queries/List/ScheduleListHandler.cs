using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using MotorcycleMaintenanceSchedule.Domain.Services.Iternal.Schedule.Queries.List;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.List;

public class ScheduleListHandler : IRequestHandler<ScheduleListParamsQuery, ActionResult>
{
    private readonly IScheduleListRepository _scheduleListRepository;

    public ScheduleListHandler(
        IScheduleListRepository scheduleListRepository)
    {
        _scheduleListRepository = scheduleListRepository;
    }

    public async Task<ActionResult> Handle(ScheduleListParamsQuery request, CancellationToken cancellationToken)
    {
        var queryParams = new ScheduleListParams
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Ascending = request.Ascending,
            OrderBy = request.OrderBy,
            Status = request.Status,
            SearchField = request.SearchField,
            SearchValue = request.SearchValue
        };

        var result = await _scheduleListRepository.List(queryParams);

        return result;
    }
}
