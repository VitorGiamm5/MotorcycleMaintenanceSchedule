using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Infrastructure.Database;
using MotorcycleMaintenanceSchedule.Infrastructure.Repositories.Schedule.BaseRepositories;

namespace MotorcycleMaintenanceSchedule.Infrastructure.Repositories.Schedule;

public class ScheduleRepository : BaseRepository<ScheduleEntity>, IScheduleRepository
{
    public ScheduleRepository(ApplicationDbContext context) : base(context)
    {
    }
}
