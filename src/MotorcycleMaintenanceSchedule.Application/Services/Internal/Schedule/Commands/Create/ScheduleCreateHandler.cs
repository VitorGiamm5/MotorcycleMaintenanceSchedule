using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Create;

public class ScheduleCreateHandler : IRequestHandler<ScheduleCreateCommand, ActionResult>
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleCreateHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<ActionResult> Handle(ScheduleCreateCommand request, CancellationToken cancellationToken)
    {
        var schedule = new ScheduleEntity
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            PhoneDDD = request.PhoneDDD,
            Observation = request.Observation,
            Status = request.Status,
            MotorcycleId = request.MotorcycleId,
            StartBusinessHour = request.StartBusinessHour,
            EndBusinessHour = request.EndBusinessHour,
            ScheduleDate = request.ScheduleDate,
            CreatedBy = "admin",
            DateCreated = DateTime.UtcNow
        };

        await _scheduleRepository.Create(schedule);

        var result = new ActionResult();

        result.SetData(schedule);

        return result;
    }
}

