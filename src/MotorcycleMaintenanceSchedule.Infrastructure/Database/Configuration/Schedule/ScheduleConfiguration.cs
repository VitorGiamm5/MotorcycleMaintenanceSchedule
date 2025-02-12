using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

namespace MotorcycleMaintenanceSchedule.Infrastructure.Database.Configuration.Schedule;

public class ScheduleConfiguration : IEntityTypeConfiguration<ScheduleEntity>
{
    public void Configure(EntityTypeBuilder<ScheduleEntity> builder)
    {
        builder.ToTable("tb_schedule");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("ID_SCHEDULE").IsRequired();
        builder.Property(t => t.Name).HasColumnName("NAME").IsRequired();
        builder.Property(t => t.Email).HasColumnName("EMAIL").IsRequired(false);
        builder.Property(t => t.Phone).HasColumnName("PHONE").IsRequired(false);
        builder.Property(t => t.PhoneDDD).HasColumnName("PHONE_DDD").IsRequired(false);
        builder.Property(t => t.Status).HasColumnName("STATUS").IsRequired();
        builder.Property(t => t.Price).HasColumnName("PRICE").IsRequired(false);

        builder.Property(t => t.MotorcycleId).HasColumnName("ID_FK_MOTORCYCLE").IsRequired(false);

        builder.Property(t => t.Observation).HasColumnName("OBSERVATION").IsRequired(false);
        builder.Property(t => t.ScheduleDate).HasColumnName("DATE_SCHEDULE").HasColumnType("timestamptz").IsRequired(false);
        builder.Property(t => t.StartBusinessHour).HasColumnName("DATE_START_BUSINESS").HasColumnType("timestamptz").IsRequired(false);
        builder.Property(t => t.EndBusinessHour).HasColumnName("DATE_END_BUSINESS").HasColumnType("timestamptz").IsRequired(false);

        builder.Property(t => t.DateCreated).HasColumnName("DATE_CREATED").IsRequired().HasColumnType("timestamptz");
        builder.Property(t => t.CreatedBy).HasColumnName("CREATED_BY").IsRequired();
        builder.Property(t => t.DateLastUpdate).HasColumnName("DATE_LAST_UPDATE").HasColumnType("timestamptz").IsRequired(false);
    }
}
