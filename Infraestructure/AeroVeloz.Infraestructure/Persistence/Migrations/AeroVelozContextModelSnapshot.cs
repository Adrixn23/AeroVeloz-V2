using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace AeroVeloz.Infraestructure.Persistence.Migrations
{
    [DbContext(typeof(AeroVelozContext))]
    partial class AeroVelozContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Audit.Audit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DataNew")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DataOld")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("IdAuditType")
                        .HasColumnType("smallint");

                    b.Property<Guid>("idUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("nameEntity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("occurentAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Audit", "Audits");
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Audit.AuditType", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<string>("nameAudit")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AuditType", "Audits");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            nameAudit = "ENTITY_CREATE"
                        },
                        new
                        {
                            Id = (short)2,
                            nameAudit = "ENTITY_UPDATE"
                        },
                        new
                        {
                            Id = (short)3,
                            nameAudit = "ENTITY_DEACTIVATE"
                        });
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Flights.Flight", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<string>("BoardingGateArrived")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BordingGate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinationAirport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginAirport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ScheduledDeparture")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("codeAirlines")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("flightStateId")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("Flight", "Flights");
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Flights.FlightHistory", b =>
                {
                    b.Property<short>("flightNumber")
                        .HasColumnType("smallint");

                    b.Property<short>("codeAirlines")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("changeAt")
                        .HasColumnType("datetime2");

                    b.Property<byte>("flightStatesIdAfter")
                        .HasColumnType("tinyint");

                    b.Property<byte>("flightStatesIdBefore")
                        .HasColumnType("tinyint");

                    b.Property<string>("reason")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("flightNumber", "codeAirlines", "changeAt");

                    b.ToTable("FlightHistory", "Flights");
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Flights.FlightState", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("codeFlightState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FlightStates", "Flights");
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Notification.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("codeProvider")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusNotification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("subscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Notification", "Notifications");
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Notification.ProviderResponse", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProviderResponse", "Notifications");
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Operations.OperationChange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("cause")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("changeAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("codeAirline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("codeAirport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("flightNumber")
                        .HasColumnType("smallint");

                    b.Property<short>("idOperationalType")
                        .HasColumnType("smallint");

                    b.Property<Guid>("idUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("newValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("previosValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OperationChange", "Operations");
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Operations.OperationalChangeType", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OperationalChangeType", "Operations");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            name = "GATE_CHANGE"
                        },
                        new
                        {
                            Id = (short)2,
                            name = "FLIGHT_DELAY"
                        },
                        new
                        {
                            Id = (short)3,
                            name = "FLIGHT_CANCELLATION"
                        });
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Organization.Airport.ConectionsAirlineAirport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("codeAirlines")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("codeAirport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("tokenApi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ConectionsAirlineAirport", "Airport");
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Organization.Base.Organizations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("emailOrganization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActived")
                        .HasColumnType("bit");

                    b.Property<string>("nameOrganization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("typeOrganization")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organizations", "Identitys");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Subscriptions.ChannelSubscriptionNotification", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChannelSubscriptionNotification", "Subscriptions");
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Subscriptions.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("activeSubscription")
                        .HasColumnType("bit");

                    b.Property<string>("codeAirlines")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("codeChannel")
                        .HasColumnType("tinyint");

                    b.Property<string>("contactValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("endingDate")
                        .HasColumnType("datetime2");

                    b.Property<short>("flightNumber")
                        .HasColumnType("smallint");

                    b.Property<int>("numberInterested")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Subscription", "Subscriptions");
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Users.Permission.Permissions", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("codePermision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions", "Identitys");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            codePermision = "ORG_CREATE",
                            description = "Crear organizaciones"
                        },
                        new
                        {
                            Id = (byte)2,
                            codePermision = "ORG_EDIT",
                            description = "Editar organizaciones"
                        },
                        new
                        {
                            Id = (byte)3,
                            codePermision = "ORG_DEACTIVATE",
                            description = "Desactivar organizaciones"
                        },
                        new
                        {
                            Id = (byte)4,
                            codePermision = "USER_CREATE",
                            description = "Crear usuarios"
                        },
                        new
                        {
                            Id = (byte)5,
                            codePermision = "USER_EDIT",
                            description = "Editar usuarios"
                        },
                        new
                        {
                            Id = (byte)6,
                            codePermision = "USER_DEACTIVATE",
                            description = "Desactivar usuarios"
                        },
                        new
                        {
                            Id = (byte)7,
                            codePermision = "AUDIT_VIEW",
                            description = "Visualizar registros de auditoría"
                        },
                        new
                        {
                            Id = (byte)8,
                            codePermision = "AIRPORT_CONN_VIEW",
                            description = "Visualizar conexiones aeropuerto-aerolínea"
                        },
                        new
                        {
                            Id = (byte)9,
                            codePermision = "AIRPORT_CONN_CREATE",
                            description = "Crear conexiones aeropuerto-aerolínea"
                        },
                        new
                        {
                            Id = (byte)10,
                            codePermision = "AIRPORT_CONN_EDIT",
                            description = "Editar conexiones aeropuerto-aerolínea"
                        },
                        new
                        {
                            Id = (byte)11,
                            codePermision = "AIRPORT_CONN_DEACTIVATE",
                            description = "Desactivar conexiones aeropuerto-aerolínea"
                        },
                        new
                        {
                            Id = (byte)12,
                            codePermision = "OP_REGISTER",
                            description = "Registrar cambios operacionales"
                        },
                        new
                        {
                            Id = (byte)13,
                            codePermision = "OP_VIEW",
                            description = "Visualizar cambios operacionales"
                        },
                        new
                        {
                            Id = (byte)14,
                            codePermision = "FLIGHT_VIEW",
                            description = "Visualizar vuelos"
                        });
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Users.Roles.Roles", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<string>("nameRol")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rol", "Identitys");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            nameRol = "SYSTEMADMIN"
                        },
                        new
                        {
                            Id = (short)2,
                            nameRol = "AIRPORTADMIN"
                        },
                        new
                        {
                            Id = (short)3,
                            nameRol = "AIRLINEADMIN"
                        },
                        new
                        {
                            Id = (short)4,
                            nameRol = "OPERATIONAIRPORT"
                        });
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Users.RolesPermision.RolPermission", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<short>("idPermission")
                        .HasColumnType("smallint");

                    b.Property<short>("idRol")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("RolPermissions", "Identitys");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            idPermission = (short)1,
                            idRol = (short)1
                        },
                        new
                        {
                            Id = (short)2,
                            idPermission = (short)2,
                            idRol = (short)1
                        },
                        new
                        {
                            Id = (short)3,
                            idPermission = (short)3,
                            idRol = (short)1
                        },
                        new
                        {
                            Id = (short)4,
                            idPermission = (short)4,
                            idRol = (short)1
                        },
                        new
                        {
                            Id = (short)5,
                            idPermission = (short)5,
                            idRol = (short)1
                        },
                        new
                        {
                            Id = (short)6,
                            idPermission = (short)6,
                            idRol = (short)1
                        },
                        new
                        {
                            Id = (short)7,
                            idPermission = (short)7,
                            idRol = (short)1
                        },
                        new
                        {
                            Id = (short)8,
                            idPermission = (short)4,
                            idRol = (short)2
                        },
                        new
                        {
                            Id = (short)9,
                            idPermission = (short)5,
                            idRol = (short)2
                        },
                        new
                        {
                            Id = (short)10,
                            idPermission = (short)6,
                            idRol = (short)2
                        },
                        new
                        {
                            Id = (short)11,
                            idPermission = (short)7,
                            idRol = (short)2
                        },
                        new
                        {
                            Id = (short)12,
                            idPermission = (short)8,
                            idRol = (short)2
                        },
                        new
                        {
                            Id = (short)13,
                            idPermission = (short)9,
                            idRol = (short)2
                        },
                        new
                        {
                            Id = (short)14,
                            idPermission = (short)10,
                            idRol = (short)2
                        },
                        new
                        {
                            Id = (short)15,
                            idPermission = (short)11,
                            idRol = (short)2
                        },
                        new
                        {
                            Id = (short)16,
                            idPermission = (short)4,
                            idRol = (short)3
                        },
                        new
                        {
                            Id = (short)17,
                            idPermission = (short)5,
                            idRol = (short)3
                        },
                        new
                        {
                            Id = (short)18,
                            idPermission = (short)6,
                            idRol = (short)3
                        },
                        new
                        {
                            Id = (short)19,
                            idPermission = (short)7,
                            idRol = (short)3
                        },
                        new
                        {
                            Id = (short)20,
                            idPermission = (short)12,
                            idRol = (short)4
                        },
                        new
                        {
                            Id = (short)21,
                            idPermission = (short)13,
                            idRol = (short)4
                        },
                        new
                        {
                            Id = (short)22,
                            idPermission = (short)14,
                            idRol = (short)4
                        });
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Users.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("failedLoginAttempts")
                        .HasColumnType("int");

                    b.Property<int>("idOrganization")
                        .HasColumnType("int");

                    b.Property<short>("idRol")
                        .HasColumnType("smallint");

                    b.Property<byte[]>("ipAdress")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("lastLoginAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("lockedUntil")
                        .HasColumnType("datetime2");

                    b.Property<string>("nameUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("passwordHash")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", "Identitys");
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Airlines.Airline", b =>
                {
                    b.HasBaseType("AeroVeloz.Domain.Entities.Organization.Base.Organizations");

                    b.Property<string>("codeAirlines")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("codeIATA")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Airlines", "Flights");
                });

            modelBuilder.Entity("AeroVeloz.Domain.Entities.Organization.Airports.Airport", b =>
                {
                    b.HasBaseType("AeroVeloz.Domain.Entities.Organization.Base.Organizations");

                    b.Property<string>("apiKeyMaster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("codeAirportIata")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("codeAirportIcao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("timeOffset")
                        .HasColumnType("datetimeoffset");

                    b.ToTable("Airports", "Airport");
                });
        }
    }
}
