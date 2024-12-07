using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Create;

public class ScheduleCreateCommand : ScheduleEntity, IRequest<ActionResult>
{
}
