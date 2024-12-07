using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Delete;

public class ScheduleDeleteCommand : IRequest<ActionResult>
{
    public required string Id { get; set; }
}
