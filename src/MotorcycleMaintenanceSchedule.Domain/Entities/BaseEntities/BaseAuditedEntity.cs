using System.Text.Json.Serialization;

namespace MotorcycleMaintenanceSchedule.Domain.Entities.BaseEntities;

public abstract class BaseAuditedEntity
{
    [JsonIgnore]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public string CreatedBy { get; set; } = "admin";

    [JsonIgnore]
    public DateTime? DateLastUpdate { get; set; }
}
