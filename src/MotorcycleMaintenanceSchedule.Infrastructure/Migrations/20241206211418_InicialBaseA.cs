using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorcycleMaintenanceSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialBaseA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbmaintenanceschedule");

            migrationBuilder.CreateTable(
                name: "tb_schedule",
                schema: "dbmaintenanceschedule",
                columns: table => new
                {
                    ID_SCHEDULE = table.Column<string>(type: "text", nullable: false),
                    NAME = table.Column<string>(type: "text", nullable: false),
                    EMAIL = table.Column<string>(type: "text", nullable: true),
                    PHONE = table.Column<int>(type: "integer", nullable: true),
                    PHONE_DDD = table.Column<int>(type: "integer", nullable: true),
                    OBSERVATION = table.Column<string>(type: "text", nullable: true),
                    STATUS = table.Column<int>(type: "integer", nullable: false),
                    ID_FK_MOTORCYCLE = table.Column<string>(type: "text", nullable: true),
                    DATE_CREATED = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    CREATED_BY = table.Column<string>(type: "text", nullable: false),
                    DATE_LAST_UPDATE = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    DATE_START_BUSINESS = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    DATE_END_BUSINESS = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    DATE_SCHEDULE = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_schedule", x => x.ID_SCHEDULE);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_schedule",
                schema: "dbmaintenanceschedule");
        }
    }
}
