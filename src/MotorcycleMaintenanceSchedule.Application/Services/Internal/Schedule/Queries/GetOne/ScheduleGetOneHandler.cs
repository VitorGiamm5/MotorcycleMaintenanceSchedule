using MediatR;
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
        try
        {
            var schedule = await _scheduleRepository.GetById(request.Id);
            if (schedule == null)
            {
                result.SetError("Schedule not found");
            }
            else
            {
                result.SetData(schedule);
            }
        }
        catch (Exception ex)
        {
            result.SetError("schedule", ex);
        }

        return result;
    }
}
