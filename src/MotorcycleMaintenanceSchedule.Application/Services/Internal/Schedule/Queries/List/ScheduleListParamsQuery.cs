using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using MotorcycleMaintenanceSchedule.Domain.Schedule;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.List;

public class ScheduleListParamsQuery : ScheduleListParams, IRequest<ActionResult>
{

}
