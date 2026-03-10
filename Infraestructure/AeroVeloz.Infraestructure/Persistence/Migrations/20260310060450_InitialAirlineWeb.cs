using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AeroVeloz.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialAirlineWeb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationalChangeTypes");

            migrationBuilder.DropTable(
                name: "OperationChanges");

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)5);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)6);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)7);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (short)8);

            migrationBuilder.DropColumn(
                name: "apiKeyMaster",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "codeAirportIata",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "codeAirportIcao",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "country",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "timeOffset",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "oldValues",
                table: "Audits");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "FlightStates",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "StateName",
                table: "FlightStates",
                newName: "codeFlightState");

            migrationBuilder.AlterColumn<short>(
                name: "Id",
                table: "Roles",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<byte>(
                name: "Id",
                table: "FlightStates",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<byte>(
                name: "flightStateId",
                table: "Flights",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProviderResponses",
                table: "ProviderResponses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightHistories",
                table: "FlightHistories",
                columns: new[] { "flightNumber", "codeAirlines", "changeAt" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConectionsAirlineAirports",
                table: "ConectionsAirlineAirports",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AuditTypes",
                columns: new[] { "Id", "nameAudit" },
                values: new object[,]
                {
                    { (short)1, "FlightStateChange" },
                    { (short)2, "FlightBatchUpload" },
                    { (short)3, "SubscriptionChange" }
                });

            migrationBuilder.InsertData(
                table: "ChannelSubscriptionNotifications",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { (byte)1, "Email" },
                    { (byte)2, "SMS" },
                    { (byte)3, "Push" }
                });

            migrationBuilder.InsertData(
                table: "FlightStates",
                columns: new[] { "Id", "codeFlightState", "name" },
                values: new object[,]
                {
                    { (byte)1, "SCHEDULED", "Scheduled" },
                    { (byte)2, "BOARDING", "Boarding" },
                    { (byte)3, "DELAYED", "Delayed" },
                    { (byte)4, "INFLIGHT", "In Flight" },
                    { (byte)5, "LANDED", "Landed" },
                    { (byte)6, "COMPLETED", "Completed" },
                    { (byte)7, "CANCELLED", "Cancelled" },
                    { (byte)8, "DIVERTED", "Diverted" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "codePermision", "description" },
                values: new object[,]
                {
                    { (byte)1, "FLIGHT_UPLOAD_BATCH", "Upload flight batch via CSV" },
                    { (byte)2, "FLIGHT_UPDATE_STATE", "Update flight state" },
                    { (byte)3, "FLIGHT_VIEW_OWN", "View own airline flights" },
                    { (byte)4, "FLIGHT_VIEW_SUBSCRIPTIONS", "View flight subscription count" },
                    { (byte)5, "CONNECTION_REQUEST", "Request airport connection" },
                    { (byte)6, "CONNECTION_VIEW", "View airline connections" }
                });

            migrationBuilder.InsertData(
                table: "ProviderResponses",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { (byte)1, "SMS" },
                    { (byte)2, "Email" },
                    { (byte)3, "Push Notification" }
                });

            migrationBuilder.InsertData(
                table: "RolPermissions",
                columns: new[] { "Id", "idPermission", "idRol" },
                values: new object[,]
                {
                    { (short)1, (short)1, (short)3 },
                    { (short)2, (short)2, (short)3 },
                    { (short)3, (short)3, (short)3 },
                    { (short)4, (short)4, (short)3 },
                    { (short)5, (short)5, (short)3 },
                    { (short)6, (short)6, (short)3 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "nameRol" },
                values: new object[] { (short)3, "AIRLINEADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProviderResponses",
                table: "ProviderResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightHistories",
                table: "FlightHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConectionsAirlineAirports",
                table: "ConectionsAirlineAirports");

            migrationBuilder.DeleteData(
                table: "AuditTypes",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "AuditTypes",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "AuditTypes",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "ChannelSubscriptionNotifications",
                keyColumn: "Id",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                table: "ChannelSubscriptionNotifications",
                keyColumn: "Id",
                keyValue: (byte)2);

            migrationBuilder.DeleteData(
                table: "ChannelSubscriptionNotifications",
                keyColumn: "Id",
                keyValue: (byte)3);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (byte)2);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (byte)3);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (byte)4);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (byte)5);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (byte)6);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (byte)7);

            migrationBuilder.DeleteData(
                table: "FlightStates",
                keyColumn: "Id",
                keyValue: (byte)8);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)5);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)6);

            migrationBuilder.DeleteData(
                table: "ProviderResponses",
                keyColumn: "Id",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                table: "ProviderResponses",
                keyColumn: "Id",
                keyValue: (byte)2);

            migrationBuilder.DeleteData(
                table: "ProviderResponses",
                keyColumn: "Id",
                keyValue: (byte)3);

            migrationBuilder.DeleteData(
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)5);

            migrationBuilder.DeleteData(
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)6);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.RenameColumn(
                name: "name",
                table: "FlightStates",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "codeFlightState",
                table: "FlightStates",
                newName: "StateName");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Roles",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "apiKeyMaster",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "codeAirportIata",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "codeAirportIcao",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "timeOffset",
                table: "Organizations",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "Id",
                table: "FlightStates",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<short>(
                name: "flightStateId",
                table: "Flights",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<string>(
                name: "oldValues",
                table: "Audits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OperationalChangeTypes",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationalChangeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    changeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    codeAirline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codeAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flightNumber = table.Column<short>(type: "smallint", nullable: true),
                    idOperationalType = table.Column<short>(type: "smallint", nullable: false),
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    newValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    previosValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.InsertData(
                table: "FlightStates",
                columns: new[] { "Id", "StateName", "code" },
                values: new object[,]
                {
                    { (short)1, "Scheduled", "SCH" },
                    { (short)2, "InProcess", "PRO" },
                    { (short)3, "Delayed", "DEL" },
                    { (short)4, "InFlight", "INF" },
                    { (short)5, "LandedArrived", "ARR" },
                    { (short)6, "Completed", "FIN" },
                    { (short)7, "Cancelled", "CAN" },
                    { (short)8, "Diverted", "DIV" }
                });
        }
    }
}
