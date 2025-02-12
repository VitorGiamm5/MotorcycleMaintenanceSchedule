using MediatR;
using MotorcycleMaintenanceSchedule.Application.Cache.Interfaces;
using MotorcycleMaintenanceSchedule.Application.Notifications.Interfaces.Schedule.Publisher;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Create;

public class ScheduleCreateHandler : IRequestHandler<ScheduleCreateCommand, ActionResult>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly INotificationSchedulePublisher _notification;
    private readonly IMotorcycleCacheService _cache;

    public ScheduleCreateHandler(
        IScheduleRepository scheduleRepository,
        INotificationSchedulePublisher notification,
        IMotorcycleCacheService motorcycleCacheService
        )
    {
        _scheduleRepository = scheduleRepository;
        _notification = notification;
        _cache = motorcycleCacheService;
    }

    public async Task<ActionResult> Handle(ScheduleCreateCommand command, CancellationToken cancellationToken)
    {
        var schedule = ScheduleCreateMappers.ToEntity(command);

        await _scheduleRepository.Create(schedule);

        var result = new ActionResult();

        result.SetData(schedule);

        var notification = ScheduleCreateMappers.ToNotification(schedule);

        _notification.PublishMotorcycle(notification);

        var scheduleSummary = ScheduleCreateMappers.ToSummary(schedule);

        await _cache.AddScheduleAsync(scheduleSummary, true);

        return result;
    }
}

