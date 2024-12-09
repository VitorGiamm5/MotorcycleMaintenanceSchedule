namespace MotorcycleMaintenanceSchedule.Domain.Services.Iternal.Schedule.Queries.List;

public enum ScheduleStatusEnum
{
    AwaitingForSchedule = 0,
    Completed = 1,
    Scheduled = 2,
    InProgress = 3,
    Overdue = 4,
}
