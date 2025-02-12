using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;

namespace MotorcycleMaintenanceSchedule.Infrastructure.Database.Configuration.Schedule;

public class NotificationScheduleConfiguration : IEntityTypeConfiguration<NotificationScheduleEntity>
{
    public void Configure(EntityTypeBuilder<NotificationScheduleEntity> builder)
    {
        builder.ToTable("tb_schedule_notification");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("ID").IsRequired();
        builder.Property(t => t.ScheduleId).HasColumnName("ID_SCHEDULE").IsRequired();
        builder.Property(t => t.MotorcyleId).HasColumnName("ID_MOTORCYCLE").IsRequired(false);
        builder.Property(t => t.Name).HasColumnName("NAME").IsRequired();
        builder.Property(t => t.Price).HasColumnName("PRICE").IsRequired();
        builder.Property(t => t.ScheduleDate).HasColumnName("SCHEDULE_DATE").IsRequired(false);
        builder.Property(t => t.Status).HasColumnName("STATUS").IsRequired();
        builder.Property(t => t.DateCreated).HasColumnName("DATE_CREATED").IsRequired().HasColumnType("timestamptz");
    }
}
