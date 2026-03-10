using System;
using Microsoft.EntityFrameworkCore.Migrations;



namespace AeroVeloz.Infraestructure.Migrations
{
    
    public partial class InitialFlightStatesMigration : Migration
    {
      
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Solo creamos las tablas que faltan y sus relaciones
            
            migrationBuilder.CreateTable(
                name: "FlightStates",
                columns: table => new
                {
                    StateID = table.Column<short>(type: "smallint", nullable: false),
                    code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightStates", x => x.StateID);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codeAirlines = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flightStateId = table.Column<short>(type: "smallint", nullable: false),
                    OriginAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledDeparture = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BordingGate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardingGateArrived = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_FlightStates_flightStateId",
                        column: x => x.flightStateId,
                        principalTable: "FlightStates",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FlightStates",
                columns: new[] { "StateID", "StateName", "code" },
                values: new object[,]
                {
                    { (short)1, "Scheduled", "Scheduled" },
                    { (short)2, "In Progress", "InProgress" },
                    { (short)3, "Delayed", "Delayed" },
                    { (short)4, "In Flight", "InFlight" },
                    { (short)5, "Landed/Arrived", "Landed" },
                    { (short)6, "Finished", "Finished" },
                    { (short)7, "Cancelled", "Cancelled" },
                    { (short)8, "Diverted", "Diverted" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_flightStateId",
                table: "Flights",
                column: "flightStateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "FlightStates");
        }
    }
}
