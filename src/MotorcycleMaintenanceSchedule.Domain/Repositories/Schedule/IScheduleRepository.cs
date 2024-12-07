﻿using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule.BaseRepositories;

namespace MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;

public interface IScheduleRepository : IBaseRepository<ScheduleEntity>
{
}