using System.Text.Json.Serialization;

namespace MotorcycleMaintenanceSchedule.Domain.Entities.BaseEntities;

public class BaseAuditedEntity
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime DateCreated { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? CreatedBy { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime? DateLastUpdate { get; set; }
}
