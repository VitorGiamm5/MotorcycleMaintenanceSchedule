using MotorcycleMaintenanceSchedule.Domain.Entities.Address;
using MotorcycleMaintenanceSchedule.Domain.Entities.BaseEntities;
using MotorcycleMaintenanceSchedule.Domain.Entities.Contacts;

namespace MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

public class ScheduleEntity : BaseAuditedEntity
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required DateTimeOffset SchedulingDate { get; set; }
    public string? Observation { get; set; }

    #region Relarionships

    public ICollection<ContactEntity>? Contacts { get; set; }

    public string? AddressId { get; set; }
    public AddressEntity? Address { get; set; }

    #endregion
}
