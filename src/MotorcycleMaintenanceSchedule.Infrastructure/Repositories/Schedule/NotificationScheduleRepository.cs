using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Infrastructure.Database;
using MotorcycleMaintenanceSchedule.Infrastructure.Repositories.BaseRepositories;

namespace MotorcycleMaintenanceSchedule.Infrastructure.Repositories.Schedule;

public class NotificationScheduleRepository : BaseRepository<NotificationScheduleEntity>, INotificationScheduleRepository
{
    public NotificationScheduleRepository(ApplicationDbContext context) : base(context)
    {
    }
}
