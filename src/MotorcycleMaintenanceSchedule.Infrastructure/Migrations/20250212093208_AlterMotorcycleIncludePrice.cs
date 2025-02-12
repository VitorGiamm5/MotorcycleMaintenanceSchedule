using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorcycleMaintenanceSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterMotorcycleIncludePrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NOTIFICATION_DATE",
                schema: "dbmaintenanceschedule",
                table: "tb_schedule_notification");

            migrationBuilder.AddColumn<DateTime>(
                name: "DATE_CREATED",
                schema: "dbmaintenanceschedule",
                table: "tb_schedule_notification",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "PRICE",
                schema: "dbmaintenanceschedule",
                table: "tb_schedule_notification",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PRICE",
                schema: "dbmaintenanceschedule",
                table: "tb_schedule",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DATE_CREATED",
                schema: "dbmaintenanceschedule",
                table: "tb_schedule_notification");

            migrationBuilder.DropColumn(
                name: "PRICE",
                schema: "dbmaintenanceschedule",
                table: "tb_schedule_notification");

            migrationBuilder.DropColumn(
                name: "PRICE",
                schema: "dbmaintenanceschedule",
                table: "tb_schedule");

            migrationBuilder.AddColumn<DateTime>(
                name: "NOTIFICATION_DATE",
                schema: "dbmaintenanceschedule",
                table: "tb_schedule_notification",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
