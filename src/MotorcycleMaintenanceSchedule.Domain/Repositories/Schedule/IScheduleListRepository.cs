using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using MotorcycleMaintenanceSchedule.Domain.Services.Iternal.Schedule.Queries.List;

namespace MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;

public interface IScheduleListRepository
{
    Task<PaginatedResult<ScheduleEntity>> List(ScheduleListParams queryParams);
    Task<PaginatedResult<ScheduleEntity>> ListAll();
}
