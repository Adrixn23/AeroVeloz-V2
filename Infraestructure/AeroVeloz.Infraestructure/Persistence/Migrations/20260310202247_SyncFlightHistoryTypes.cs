using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroVeloz.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SyncFlightHistoryTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightHistories",
                table: "FlightHistories");

            migrationBuilder.RenameTable(
                name: "FlightHistories",
                newName: "FlightHistory");

            migrationBuilder.AlterColumn<byte>(
                name: "flightStatesIdBefore",
                table: "FlightHistory",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<byte>(
                name: "flightStatesIdAfter",
                table: "FlightHistory",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightHistory",
                table: "FlightHistory",
                columns: new[] { "flightNumber", "codeAirlines", "changeAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightHistory",
                table: "FlightHistory");

            migrationBuilder.RenameTable(
                name: "FlightHistory",
                newName: "FlightHistories");

            migrationBuilder.AlterColumn<short>(
                name: "flightStatesIdBefore",
                table: "FlightHistories",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<short>(
                name: "flightStatesIdAfter",
                table: "FlightHistories",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightHistories",
                table: "FlightHistories",
                columns: new[] { "flightNumber", "codeAirlines", "changeAt" });
        }
    }
}
