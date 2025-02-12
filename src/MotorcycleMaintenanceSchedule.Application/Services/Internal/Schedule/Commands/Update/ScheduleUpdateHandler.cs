using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Update;

public class ScheduleUpdateHandler : IRequestHandler<ScheduleUpdateCommand, ActionResult>
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleUpdateHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<ActionResult> Handle(ScheduleUpdateCommand command, CancellationToken cancellationToken)
    {
        var result = new ActionResult();

        try
        {
            var schedule = await _scheduleRepository.GetById(command.Id);

            if (schedule == null)
            {
                result.SetError("Schedule", FaultMessagesConst.MESSAGE_ERROR_NOT_FOUND);

                return result;
            }

            var updatedSchedule = ScheduleUpdateMappers.Map(command);

            await _scheduleRepository.Update(updatedSchedule);

            result.SetData(updatedSchedule);
        }
        catch (Exception ex)
        {
            result.SetError("An error occurred while updating the schedule", ex.Message);
        }

        return result;
    }
}
