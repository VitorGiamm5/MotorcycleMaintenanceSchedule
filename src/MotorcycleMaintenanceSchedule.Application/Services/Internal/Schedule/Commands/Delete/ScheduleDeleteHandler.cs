using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Delete;

public class ScheduleDeleteHandler : IRequestHandler<ScheduleDeleteCommand, ActionResult>
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleDeleteHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<ActionResult> Handle(ScheduleDeleteCommand request, CancellationToken cancellationToken)
    {
        var result = new ActionResult();

        try
        {
            var schedule = await _scheduleRepository.GetById(request.Id);
            if (schedule == null)
            {
                result.SetError("Schedule", FaultMessagesConst.MESSAGE_ERROR_NOT_FOUND);
                return result;
            }

            await _scheduleRepository.Delete(request.Id);
        }
        catch (Exception ex)
        {
            result.SetError("An error occurred while deleting the schedule", ex.Message);
        }

        return result;
    }
}
