using MotorcycleMaintenanceSchedule.Domain.Entities.Address;
using MotorcycleMaintenanceSchedule.Domain.Entities.BaseEntities;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

namespace MotorcycleMaintenanceSchedule.Domain.Entities.Contacts;

public class ContactEntity : BaseAuditedEntity
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required int Phone { get; set; }
    public required int PhoneDDD { get; set; }

    #region relationships
    
    public string? AdressId { get; set; }
    public AddressEntity? Adress { get; set; }

    public string? ScheduleId { get; set; }
    public ScheduleEntity? Schedule { get; set; }

    #endregion
}
