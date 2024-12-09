using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.GetOne;

public class ScheduleGetOneQuery : IRequest<ActionResult>
{
    public required string Id { get; set; }
}
