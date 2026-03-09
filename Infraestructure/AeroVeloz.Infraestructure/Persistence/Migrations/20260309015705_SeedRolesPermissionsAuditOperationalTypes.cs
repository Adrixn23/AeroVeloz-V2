using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AeroVeloz.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesPermissionsAuditOperationalTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAuditType = table.Column<short>(type: "smallint", nullable: false),
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nameEntity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    occurentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNew = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditTypes",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameAudit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChannelSubscriptionNotifications",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelSubscriptionNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConectionsAirlineAirports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codeAirlines = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codeAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tokenApi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConectionsAirlineAirports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightHistories",
                columns: table => new
                {
                    flightNumber = table.Column<short>(type: "smallint", nullable: false),
                    codeAirlines = table.Column<short>(type: "smallint", nullable: false),
                    changeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flightStatesIdAfter = table.Column<byte>(type: "tinyint", nullable: false),
                    flightStatesIdBefore = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightHistories", x => new { x.flightNumber, x.codeAirlines, x.changeAt });
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codeAirlines = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flightStateId = table.Column<byte>(type: "tinyint", nullable: false),
                    OriginAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledDeparture = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BordingGate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardingGateArrived = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightStates",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    codeFlightState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codeProvider = table.Column<byte>(type: "tinyint", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    statusNotification = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

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
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idOperationalType = table.Column<short>(type: "smallint", nullable: false),
                    flightNumber = table.Column<short>(type: "smallint", nullable: false),
                    codeAirline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codeAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    previosValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    changeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationChanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameOrganization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    typeOrganization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActived = table.Column<bool>(type: "bit", nullable: false),
                    emailOrganization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    codeAirlines = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codeIATA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codeAirportIcao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codeAirportIata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    apiKeyMaster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timeOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    codePermision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProviderResponses",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderResponses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameRol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolPermissions",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idRol = table.Column<short>(type: "smallint", nullable: false),
                    idPermission = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    flightNumber = table.Column<short>(type: "smallint", nullable: false),
                    codeAirlines = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codeChannel = table.Column<byte>(type: "tinyint", nullable: false),
                    numberInterested = table.Column<int>(type: "int", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    activeSubscription = table.Column<bool>(type: "bit", nullable: false),
                    contactValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nameUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    passwordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    ipAdress = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    lastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    failedLoginAttempts = table.Column<int>(type: "int", nullable: false),
                    lockedUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    idOrganization = table.Column<int>(type: "int", nullable: false),
                    idRol = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AuditTypes",
                columns: new[] { "Id", "nameAudit" },
                values: new object[,]
                {
                    { (short)1, "ENTITY_CREATE" },
                    { (short)2, "ENTITY_UPDATE" },
                    { (short)3, "ENTITY_DEACTIVATE" }
                });

            migrationBuilder.InsertData(
                table: "OperationalChangeTypes",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { (short)1, "GATE_CHANGE" },
                    { (short)2, "FLIGHT_DELAY" },
                    { (short)3, "FLIGHT_CANCELLATION" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "codePermision", "description" },
                values: new object[,]
                {
                    { (byte)1, "ORG_CREATE", "Crear organizaciones" },
                    { (byte)2, "ORG_EDIT", "Editar organizaciones" },
                    { (byte)3, "ORG_DEACTIVATE", "Desactivar organizaciones" },
                    { (byte)4, "USER_CREATE", "Crear usuarios" },
                    { (byte)5, "USER_EDIT", "Editar usuarios" },
                    { (byte)6, "USER_DEACTIVATE", "Desactivar usuarios" },
                    { (byte)7, "AUDIT_VIEW", "Visualizar registros de auditoría" },
                    { (byte)8, "AIRPORT_CONN_VIEW", "Visualizar conexiones aeropuerto-aerolínea" },
                    { (byte)9, "AIRPORT_CONN_CREATE", "Crear conexiones aeropuerto-aerolínea" },
                    { (byte)10, "AIRPORT_CONN_EDIT", "Editar conexiones aeropuerto-aerolínea" },
                    { (byte)11, "AIRPORT_CONN_DEACTIVATE", "Desactivar conexiones aeropuerto-aerolínea" },
                    { (byte)12, "OP_REGISTER", "Registrar cambios operacionales" },
                    { (byte)13, "OP_VIEW", "Visualizar cambios operacionales" },
                    { (byte)14, "FLIGHT_VIEW", "Visualizar vuelos" }
                });

            migrationBuilder.InsertData(
                table: "RolPermissions",
                columns: new[] { "Id", "idPermission", "idRol" },
                values: new object[,]
                {
                    { (short)1, (short)1, (short)1 },
                    { (short)2, (short)2, (short)1 },
                    { (short)3, (short)3, (short)1 },
                    { (short)4, (short)4, (short)1 },
                    { (short)5, (short)5, (short)1 },
                    { (short)6, (short)6, (short)1 },
                    { (short)7, (short)7, (short)1 },
                    { (short)8, (short)4, (short)2 },
                    { (short)9, (short)5, (short)2 },
                    { (short)10, (short)6, (short)2 },
                    { (short)11, (short)7, (short)2 },
                    { (short)12, (short)8, (short)2 },
                    { (short)13, (short)9, (short)2 },
                    { (short)14, (short)10, (short)2 },
                    { (short)15, (short)11, (short)2 },
                    { (short)16, (short)4, (short)3 },
                    { (short)17, (short)5, (short)3 },
                    { (short)18, (short)6, (short)3 },
                    { (short)19, (short)7, (short)3 },
                    { (short)20, (short)12, (short)4 },
                    { (short)21, (short)13, (short)4 },
                    { (short)22, (short)14, (short)4 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "nameRol" },
                values: new object[,]
                {
                    { (short)1, "SYSTEMADMIN" },
                    { (short)2, "AIRPORTADMIN" },
                    { (short)3, "AIRLINEADMIN" },
                    { (short)4, "OPERATIONAIRPORT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "AuditTypes");

            migrationBuilder.DropTable(
                name: "ChannelSubscriptionNotifications");

            migrationBuilder.DropTable(
                name: "ConectionsAirlineAirports");

            migrationBuilder.DropTable(
                name: "FlightHistories");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "FlightStates");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OperationalChangeTypes");

            migrationBuilder.DropTable(
                name: "OperationChanges");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "ProviderResponses");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "RolPermissions");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
