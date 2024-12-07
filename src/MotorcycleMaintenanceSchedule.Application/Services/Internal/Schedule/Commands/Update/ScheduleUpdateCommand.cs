using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Update;

public class ScheduleUpdateCommand : ScheduleEntity, IRequest<ActionResult>
{
}
