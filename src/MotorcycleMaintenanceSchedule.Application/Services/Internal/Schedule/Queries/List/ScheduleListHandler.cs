using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.List;

public class ScheduleListHandler : IRequestHandler<ScheduleListParamsQuery, ActionResult>
{
    private readonly IScheduleListRepository _scheduleListRepository;

    public ScheduleListHandler(
        IScheduleListRepository scheduleListRepository
        )
    {
        _scheduleListRepository = scheduleListRepository;
    }

    public async Task<ActionResult> Handle(ScheduleListParamsQuery request, CancellationToken cancellationToken)
    {
        var queryParams = ScheduleListMappers.Map(request);

        var result = await _scheduleListRepository.List(queryParams);

        return result;
    }
}
