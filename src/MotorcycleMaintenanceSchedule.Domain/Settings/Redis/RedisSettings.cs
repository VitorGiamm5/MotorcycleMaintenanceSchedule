namespace MotorcycleMaintenanceSchedule.Domain.Settings.Redis;

public sealed class RedisSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
}
