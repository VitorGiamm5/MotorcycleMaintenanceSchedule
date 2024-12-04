using MotorcycleMaintenanceSchedule.Domain.Entities.BaseEntities;

namespace MotorcycleMaintenanceSchedule.Domain.Entities.Address;

public class AddressEntity : BaseAuditedEntity
{
    public required string Id { get; set; }
    public required string Street { get; set; }
    public required string ZipCode { get; set; }
    public required string Number { get; set; }
    public required string Neighborhood { get; set; }

    #region relationships

    public required string CityId { get; set; }
    public string? CityName { get; set; }

    #endregion
}
