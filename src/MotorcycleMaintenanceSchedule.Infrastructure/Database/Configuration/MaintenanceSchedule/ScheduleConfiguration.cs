using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

namespace MotorcycleMaintenanceSchedule.Infrastructure.Database.Configuration.MaintenanceSchedule;

public class ScheduleConfiguration : IEntityTypeConfiguration<ScheduleEntity>
{
    public void Configure(EntityTypeBuilder<ScheduleEntity> entity)
    {
        entity.ToTable("tb_schedule");

        entity.HasKey(t => t.Id);

        entity.Property(t => t.Id).HasColumnName("ID_SCHEDULE");
        entity.Property(t => t.Name).HasColumnName("NAME").IsRequired();
        entity.Property(t => t.Email).HasColumnName("EMAIL").IsRequired(false);
        entity.Property(t => t.Phone).HasColumnName("PHONE").IsRequired(false);
        entity.Property(t => t.PhoneDDD).HasColumnName("PHONE_DDD").IsRequired(false);
        entity.Property(t => t.Status).HasColumnName("STATUS").IsRequired();

        entity.Property(t => t.MotorcycleId).HasColumnName("ID_FK_MOTORCYCLE").IsRequired(false);

        entity.Property(t => t.Observation).HasColumnName("OBSERVATION").IsRequired(false);
        entity.Property(t => t.ScheduleDate).HasColumnName("DATE_SCHEDULE").IsRequired(false).HasColumnType("timestamptz");
        entity.Property(t => t.StartBusinessHour).HasColumnName("DATE_START_BUSINESS").IsRequired(false).HasColumnType("timestamptz");
        entity.Property(t => t.EndBusinessHour).HasColumnName("DATE_END_BUSINESS").IsRequired(false).HasColumnType("timestamptz");

        entity.Property(t => t.DateCreated).HasColumnName("DATE_CREATED").IsRequired().HasColumnType("timestamptz");
        entity.Property(t => t.CreatedBy).HasColumnName("CREATED_BY").IsRequired();
        entity.Property(t => t.DateLastUpdate).HasColumnName("DATE_LAST_UPDATE").IsRequired(false).HasColumnType("timestamptz");
    }
}
