using Microsoft.EntityFrameworkCore.ChangeTracking;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.MaintenceSchedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using MotorcycleMaintenanceSchedule.Infrastructure.Database;
using MotorcycleMaintenanceSchedule.Infrastructure.Repositories.BaseRepositories;

namespace MotorcycleMaintenanceSchedule.Infrastructure.Repositories.MaintenceSchedule;

public class ScheduleRepository : BaseRepository<ScheduleEntity>
{
    public ScheduleRepository(ApplicationDbContext context) : base(context)
    {
    }
}
