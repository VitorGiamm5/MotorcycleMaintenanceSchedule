using MotorcycleMaintenanceSchedule.Domain.Cache.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Request.BaseRequest;

namespace MotorcycleMaintenanceSchedule.Application.Cache.Interfaces;

public interface IMotorcycleCacheService
{
    Task AddScheduleAsync(ScheduleSummaryDto schedule, bool autoUpdateSummary);
    Task CreateOrUpdateSummaryAsync(ScheduleSummaryDto schedule);
    Task<DailySummaryDto?> GetDailySummaryByDateAsync(string date);
    Task<List<ScheduleSummaryDto>> GetMotorcyclesByIdAsync(string motorcycleId);
    Task<List<ScheduleSummaryDto>> GetMotorcyclesByDateRangeAsync(BaseQueryParams query);
}
