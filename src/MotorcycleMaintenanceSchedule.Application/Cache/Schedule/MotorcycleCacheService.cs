using MotorcycleMaintenanceSchedule.Application.Cache.Interfaces;
using MotorcycleMaintenanceSchedule.Domain.Cache.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Request.BaseRequest;
using StackExchange.Redis;
using System.Text.Json;

namespace MotorcycleMaintenanceSchedule.Application.Cache.Schedule;

public class MotorcycleCacheService : IMotorcycleCacheService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly string _prefix = "schedule";
    private readonly int _expirationSchedulesDays = 5;
    private readonly int _expirationDailyDays = 7;

    public MotorcycleCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task AddScheduleAsync(ScheduleSummaryDto schedule, bool autoUpdateSummary)
    {
        var db = _redis.GetDatabase();

        var motorcycleKey = $"{_prefix}-{schedule.ScheduleId}-{schedule.Date}";

        var jsonData = JsonSerializer.Serialize(schedule);

        await db.StringSetAsync(motorcycleKey, jsonData);

        await db.KeyExpireAsync(motorcycleKey, TimeSpan.FromDays(_expirationSchedulesDays));

        if (autoUpdateSummary)
        {
            await CreateOrUpdateSummaryAsync(schedule);
        }
    }

    public async Task<DailySummaryDto?> GetDailySummaryByDateAsync(string date)
    {
        var db = _redis.GetDatabase();

        var totalizerKey = $"{_prefix}-daily-{date}";

        var data = await db.StringGetAsync(totalizerKey);

        var result = data.HasValue
            ? JsonSerializer.Deserialize<DailySummaryDto>(data!)
            : null;

        return result;
    }

    public async Task<List<ScheduleSummaryDto>> GetMotorcyclesByDateRangeAsync(BaseQueryParams query)
    {
        var db = _redis.GetDatabase();

        var server = _redis.GetServer(_redis.GetEndPoints()[0]);

        var results = new List<ScheduleSummaryDto>();

        DateTime date = query.CreationDateStart ?? DateTime.UtcNow;

        for (var inicialDate = date; inicialDate <= query.CreationDateEnd; inicialDate = date.AddDays(1))
        {
            var keys = server.Keys(pattern: $"{_prefix}-*-{date}");

            foreach (var key in keys)
            {
                var data = await db.StringGetAsync(key);

                if (data.HasValue)
                {
                    var summary = JsonSerializer.Deserialize<ScheduleSummaryDto>(data!);

                    if (summary != null)
                    {
                        results.Add(summary);
                    }
                }
            }
        }

        return results;
    }

    public async Task<List<ScheduleSummaryDto>> GetMotorcyclesByIdAsync(string motorcycleId)
    {
        var db = _redis.GetDatabase();

        var server = _redis.GetServer(_redis.GetEndPoints()[0]);

        var keys = server.Keys(pattern: $"{_prefix}-{motorcycleId}-*");

        var results = new List<ScheduleSummaryDto>();

        foreach (var key in keys)
        {
            var data = await db.StringGetAsync(key);

            if (data.HasValue)
            {
                var summary = JsonSerializer.Deserialize<ScheduleSummaryDto>(data!);

                if (summary != null)
                {
                    results.Add(summary);
                }
            }
        }

        return results;
    }

    public async Task CreateOrUpdateSummaryAsync(ScheduleSummaryDto schedule)
    {
        var db = _redis.GetDatabase();

        var totalizerKey = $"{_prefix}-daily-{schedule.Date}";

        var existingData = await _redis.GetDatabase().StringGetAsync(totalizerKey);

        DailySummaryDto dailySummary;

        if (existingData.HasValue)
        {
            dailySummary = JsonSerializer.Deserialize<DailySummaryDto>(existingData!)!;
            dailySummary.TotalPrice += schedule.Price;
            dailySummary.TotalMotorcycles += 1;
        }
        else
        {
            dailySummary = new DailySummaryDto
            {
                Date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                TotalPrice = schedule.Price,
                TotalMotorcycles = 1
            };
        }

        var jsonData = JsonSerializer.Serialize(dailySummary);

        await db.StringSetAsync(totalizerKey, jsonData);

        await db.KeyExpireAsync(totalizerKey, TimeSpan.FromDays(_expirationDailyDays));
    }
}

