using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AeroVeloz.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Flights");

            migrationBuilder.EnsureSchema(
                name: "Airport");

            migrationBuilder.EnsureSchema(
                name: "Audits");

            migrationBuilder.EnsureSchema(
                name: "Subscriptions");

            migrationBuilder.EnsureSchema(
                name: "Notifications");

            migrationBuilder.EnsureSchema(
                name: "Operations");

            migrationBuilder.EnsureSchema(
                name: "Identitys");

            migrationBuilder.CreateTable(
                name: "Audit",
                schema: "Audits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAuditType = table.Column<short>(type: "smallint", nullable: false),
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nameEntity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ocurrentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    newValuesData = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditType",
                schema: "Audits",
                columns: table => new
                {
                    idAuditType = table.Column<short>(type: "smallint", nullable: false),
                    nameAudit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditType", x => x.idAuditType);
                });

            migrationBuilder.CreateTable(
                name: "ChannelSubscriptionNotification",
                schema: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelSubscriptionNotification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConectionsAirlineAirport",
                schema: "Airport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codeAirlinesIcao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codeAirportIcao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tokenApi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConectionsAirlineAirport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                schema: "Flights",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false),
                    codeAirlinesIcao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flightStatesId = table.Column<byte>(type: "tinyint", nullable: false),
                    OriginAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledDeparture = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BordingGate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardingGateArrived = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightHistory",
                schema: "Flights",
                columns: table => new
                {
                    flightNumber = table.Column<short>(type: "smallint", nullable: false),
                    codeAirlines = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    changeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flightStatesIdAfter = table.Column<byte>(type: "tinyint", nullable: false),
                    flightStatesIdBefore = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightHistory", x => new { x.flightNumber, x.codeAirlines });
                });

            migrationBuilder.CreateTable(
                name: "FlightStates",
                schema: "Flights",
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
                name: "Notification",
                schema: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subscripcionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codeProvider = table.Column<short>(type: "smallint", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    statusNotification = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationalChangeType",
                schema: "Operations",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationalChangeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationChange",
                schema: "Operations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idOperationalType = table.Column<short>(type: "smallint", nullable: false),
                    flightNumber = table.Column<short>(type: "smallint", nullable: false),
                    codeAirlinesIcao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codeAirportIcao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    previosValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    changeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationChange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "Identitys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameOrganization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    typeOrganization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActived = table.Column<bool>(type: "bit", nullable: false),
                    emailOrganization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "Identitys",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false),
                    codePermision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProviderResponse",
                schema: "Notifications",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderResponse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                schema: "Identitys",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false),
                    nameRol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolPermissions",
                schema: "Identitys",
                columns: table => new
                {
                    idRolPermission = table.Column<short>(type: "smallint", nullable: false),
                    idRol = table.Column<short>(type: "smallint", nullable: false),
                    idPermission = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermissions", x => x.idRolPermission);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                schema: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    flightNumber = table.Column<short>(type: "smallint", nullable: false),
                    codeAirlinesIcao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codeChannel = table.Column<byte>(type: "tinyint", nullable: false),
                    numberInterested = table.Column<int>(type: "int", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    activeSubscription = table.Column<bool>(type: "bit", nullable: false),
                    contactValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Identitys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nameUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    passwordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    ipAdress = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    lastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    failedLoginAttempts = table.Column<int>(type: "int", nullable: true),
                    lockedUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    idOrganization = table.Column<int>(type: "int", nullable: false),
                    idRol = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Airlines",
                schema: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    codeAirlines = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codeIATA = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Airlines_Organizations_Id",
                        column: x => x.Id,
                        principalSchema: "Identitys",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                schema: "Airport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    codeAirportIcao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codeAirportIata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    apiKeyMaster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timeOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Airports_Organizations_Id",
                        column: x => x.Id,
                        principalSchema: "Identitys",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Audits",
                table: "AuditType",
                columns: new[] { "idAuditType", "nameAudit" },
                values: new object[,]
                {
                    { (short)1, "ENTITY_CREATE" },
                    { (short)2, "ENTITY_UPDATE" },
                    { (short)3, "ENTITY_DEACTIVATE" }
                });

            migrationBuilder.InsertData(
                schema: "Subscriptions",
                table: "ChannelSubscriptionNotification",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { (byte)1, "Email" },
                    { (byte)2, "SMS" },
                    { (byte)3, "Push" }
                });

            migrationBuilder.InsertData(
                schema: "Flights",
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
                schema: "Identitys",
                table: "Organizations",
                columns: new[] { "Id", "createAt", "emailOrganization", "isActived", "nameOrganization", "typeOrganization" },
                values: new object[] { 1, new DateTime(2026, 3, 16, 23, 16, 23, 32, DateTimeKind.Local).AddTicks(2878), "Admin@Aeroveloz.com", true, "aerovelozGlobal", "admin" });

            migrationBuilder.InsertData(
                schema: "Identitys",
                table: "Permissions",
                columns: new[] { "Id", "codePermision", "description" },
                values: new object[,]
                {
                    { (short)1, "ORG_CREATE", "Crear organizaciones" },
                    { (short)2, "ORG_EDIT", "Editar organizaciones" },
                    { (short)3, "ORG_DEACTIVATE", "Desactivar organizaciones" },
                    { (short)4, "USER_CREATE", "Crear usuarios" },
                    { (short)5, "USER_EDIT", "Editar usuarios" },
                    { (short)6, "USER_DEACTIVATE", "Desactivar usuarios" },
                    { (short)7, "AUDIT_VIEW", "Visualizar registros de auditoría" },
                    { (short)8, "AIRPORT_CONN_VIEW", "Visualizar conexiones aeropuerto-aerolínea" },
                    { (short)9, "AIRPORT_CONN_CREATE", "Crear conexiones aeropuerto-aerolínea" },
                    { (short)10, "AIRPORT_CONN_EDIT", "Editar conexiones aeropuerto-aerolínea" },
                    { (short)11, "AIRPORT_CONN_DEACTIVATE", "Desactivar conexiones aeropuerto-aerolínea" },
                    { (short)12, "OP_REGISTER", "Registrar cambios operacionales" },
                    { (short)13, "OP_VIEW", "Visualizar cambios operacionales" },
                    { (short)14, "FLIGHT_VIEW", "Visualizar vuelos" }
                });

            migrationBuilder.InsertData(
                schema: "Notifications",
                table: "ProviderResponse",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { (short)1, "SMS" },
                    { (short)2, "Email" },
                    { (short)3, "Push Notification" }
                });

            migrationBuilder.InsertData(
                schema: "Identitys",
                table: "Rol",
                columns: new[] { "Id", "nameRol" },
                values: new object[,]
                {
                    { (short)1, "SYSTEMADMIN" },
                    { (short)2, "AIRPORTADMIN" },
                    { (short)3, "AIRLINEADMIN" },
                    { (short)4, "OPERATIONAIRPORT" }
                });

            migrationBuilder.InsertData(
                schema: "Identitys",
                table: "RolPermissions",
                columns: new[] { "idRolPermission", "idPermission", "idRol" },
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airlines",
                schema: "Flights");

            migrationBuilder.DropTable(
                name: "Airports",
                schema: "Airport");

            migrationBuilder.DropTable(
                name: "Audit",
                schema: "Audits");

            migrationBuilder.DropTable(
                name: "AuditType",
                schema: "Audits");

            migrationBuilder.DropTable(
                name: "ChannelSubscriptionNotification",
                schema: "Subscriptions");

            migrationBuilder.DropTable(
                name: "ConectionsAirlineAirport",
                schema: "Airport");

            migrationBuilder.DropTable(
                name: "Flight",
                schema: "Flights");

            migrationBuilder.DropTable(
                name: "FlightHistory",
                schema: "Flights");

            migrationBuilder.DropTable(
                name: "FlightStates",
                schema: "Flights");

            migrationBuilder.DropTable(
                name: "Notification",
                schema: "Notifications");

            migrationBuilder.DropTable(
                name: "OperationalChangeType",
                schema: "Operations");

            migrationBuilder.DropTable(
                name: "OperationChange",
                schema: "Operations");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "Identitys");

            migrationBuilder.DropTable(
                name: "ProviderResponse",
                schema: "Notifications");

            migrationBuilder.DropTable(
                name: "Rol",
                schema: "Identitys");

            migrationBuilder.DropTable(
                name: "RolPermissions",
                schema: "Identitys");

            migrationBuilder.DropTable(
                name: "Subscription",
                schema: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Identitys");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "Identitys");
        }
    }
}
