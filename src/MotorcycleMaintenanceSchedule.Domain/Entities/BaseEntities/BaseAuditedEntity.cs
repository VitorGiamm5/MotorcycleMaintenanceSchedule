using System.Text.Json.Serialization;

namespace MotorcycleMaintenanceSchedule.Domain.Entities.BaseEntities;

public class BaseAuditedEntity
{
    [JsonIgnore]
    public DateTime DateCreated { get; set; } = DateTime.Now;

    [JsonIgnore]
    public string CreatedBy { get; set; } = "admin";

    [JsonIgnore]
    public DateTime? DateLastUpdate { get; set; }
}
