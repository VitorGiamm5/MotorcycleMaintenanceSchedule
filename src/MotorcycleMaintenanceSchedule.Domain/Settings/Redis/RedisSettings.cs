namespace MotorcycleMaintenanceSchedule.Domain.Settings.Redis;

public class RedisSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string InstanceName { get; set; } = string.Empty;
}
