using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Exceptions;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.GetOne;

public class ScheduleGetOneHandler : IRequestHandler<ScheduleGetOneQuery, ActionResult>
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleGetOneHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<ActionResult> Handle(ScheduleGetOneQuery request, CancellationToken cancellationToken)
    {
        var result = new ActionResult();

        var schedule = await _scheduleRepository.GetById(request.Id);

        if (schedule?.Id == null)
        {
            result.SetError("Schedule", FaultMessagesConst.MESSAGE_ERROR_NOT_FOUND);

            return result;
        }

        result.SetData(schedule);

        return result;
    }
}
