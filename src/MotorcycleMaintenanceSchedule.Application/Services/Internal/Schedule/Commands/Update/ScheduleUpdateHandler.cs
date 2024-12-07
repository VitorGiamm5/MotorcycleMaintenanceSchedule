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

    public async Task<ActionResult> Handle(ScheduleUpdateCommand request, CancellationToken cancellationToken)
    {
        var result = new ActionResult();

        try
        {
            var schedule = await _scheduleRepository.GetById(request.Id);
            if (schedule == null)
            {
                result.SetError("Schedule not found");
                return result;
            }

            schedule.Name = request.Name;
            schedule.Email = request.Email;
            schedule.Phone = request.Phone;
            schedule.PhoneDDD = request.PhoneDDD;
            schedule.Observation = request.Observation;
            schedule.Status = request.Status;
            schedule.MotorcycleId = request.MotorcycleId;
            schedule.StartBusinessHour = request.StartBusinessHour;
            schedule.EndBusinessHour = request.EndBusinessHour;
            schedule.ScheduleDate = request.ScheduleDate;
            schedule.DateLastUpdate = DateTime.UtcNow;

            await _scheduleRepository.Update(schedule);

            result.SetData(schedule);
        }
        catch (Exception ex)
        {
            result.SetError("An error occurred while updating the schedule", ex.Message);
        }

        return result;
    }
}
