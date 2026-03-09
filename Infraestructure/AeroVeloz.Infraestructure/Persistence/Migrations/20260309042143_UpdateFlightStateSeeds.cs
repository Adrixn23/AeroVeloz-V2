using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroVeloz.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFlightStateSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_FlightStates_flightStateId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_flightStateId",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "StateID",
                table: "FlightStates",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "FlightStates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StateName",
                table: "FlightStates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "Id",
                table: "FlightStates",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint")
                .Annotation("SqlServer:Identity", "1, 1");

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

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "code",
                value: "SCH");

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)2,
                columns: new[] { "StateName", "code" },
                values: new object[] { "InProcess", "PRO" });

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)3,
                column: "code",
                value: "DEL");

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)4,
                columns: new[] { "StateName", "code" },
                values: new object[] { "InFlight", "INF" });

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)5,
                columns: new[] { "StateName", "code" },
                values: new object[] { "LandedArrived", "ARR" });

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)6,
                columns: new[] { "StateName", "code" },
                values: new object[] { "Completed", "FIN" });

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)7,
                column: "code",
                value: "CAN");

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)8,
                column: "code",
                value: "DIV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FlightStates",
                newName: "StateID");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "FlightStates",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StateName",
                table: "FlightStates",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "StateID",
                table: "FlightStates",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<byte>(
                name: "flightStatesIdBefore",
                table: "FlightHistories",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<byte>(
                name: "flightStatesIdAfter",
                table: "FlightHistories",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "StateID",
                keyValue: (short)1,
                column: "code",
                value: "Scheduled");

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "StateID",
                keyValue: (short)2,
                columns: new[] { "StateName", "code" },
                values: new object[] { "In Progress", "InProgress" });

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "StateID",
                keyValue: (short)3,
                column: "code",
                value: "Delayed");

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "StateID",
                keyValue: (short)4,
                columns: new[] { "StateName", "code" },
                values: new object[] { "In Flight", "InFlight" });

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "StateID",
                keyValue: (short)5,
                columns: new[] { "StateName", "code" },
                values: new object[] { "Landed/Arrived", "Landed" });

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "StateID",
                keyValue: (short)6,
                columns: new[] { "StateName", "code" },
                values: new object[] { "Finished", "Finished" });

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "StateID",
                keyValue: (short)7,
                column: "code",
                value: "Cancelled");

            migrationBuilder.UpdateData(
                table: "FlightStates",
                keyColumn: "StateID",
                keyValue: (short)8,
                column: "code",
                value: "Diverted");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_flightStateId",
                table: "Flights",
                column: "flightStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_FlightStates_flightStateId",
                table: "Flights",
                column: "flightStateId",
                principalTable: "FlightStates",
                principalColumn: "StateID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
