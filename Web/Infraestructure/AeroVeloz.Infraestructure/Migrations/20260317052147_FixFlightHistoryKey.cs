using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroVeloz.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class FixFlightHistoryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightHistory",
                schema: "Flights",
                table: "FlightHistory");

            migrationBuilder.RenameColumn(
                name: "codeIATA",
                schema: "Flights",
                table: "Airlines",
                newName: "codeIata");

            migrationBuilder.RenameColumn(
                name: "codeAirlines",
                schema: "Flights",
                table: "Airlines",
                newName: "codeAirlinesIcao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightHistory",
                schema: "Flights",
                table: "FlightHistory",
                columns: new[] { "flightNumber", "codeAirlines", "changeAt" });

            migrationBuilder.UpdateData(
                schema: "Identitys",
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1,
                column: "createAt",
                value: new DateTime(2026, 3, 17, 1, 21, 46, 567, DateTimeKind.Local).AddTicks(9465));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightHistory",
                schema: "Flights",
                table: "FlightHistory");

            migrationBuilder.RenameColumn(
                name: "codeIata",
                schema: "Flights",
                table: "Airlines",
                newName: "codeIATA");

            migrationBuilder.RenameColumn(
                name: "codeAirlinesIcao",
                schema: "Flights",
                table: "Airlines",
                newName: "codeAirlines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightHistory",
                schema: "Flights",
                table: "FlightHistory",
                columns: new[] { "flightNumber", "codeAirlines" });

            migrationBuilder.UpdateData(
                schema: "Identitys",
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1,
                column: "createAt",
                value: new DateTime(2026, 3, 16, 23, 16, 23, 32, DateTimeKind.Local).AddTicks(2878));
        }
    }
}
