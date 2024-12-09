using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Exceptions;
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

                throw new BussinessExceptionExtension("schedule".);
            }

            schedule.Id = schedule.Id;
            schedule.Name = schedule.Name;
            schedule.Email = schedule.Email;
            schedule.Phone = schedule.Phone;
            schedule.PhoneDDD = schedule.PhoneDDD;
            schedule.Observation = schedule.Observation;
            schedule.Status = schedule.Status;
            schedule.MotorcycleId = schedule.MotorcycleId;
            schedule.StartBusinessHour = schedule.StartBusinessHour;
            schedule.EndBusinessHour = schedule.EndBusinessHour;
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
