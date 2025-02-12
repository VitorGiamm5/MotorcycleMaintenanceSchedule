using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using MotorcycleMaintenanceSchedule.Domain.Services.Internal.Schedule.Queries.List;

namespace MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;

public interface IScheduleListRepository
{
    Task<ActionResult> List(ScheduleListParams queryParams);
}
