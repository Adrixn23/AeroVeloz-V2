using System;
using System.Collections.Generic;
using AeroVeloz.Infraestructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.context;

public partial class AeroVelozDbContext : DbContext
{
    public AeroVelozDbContext()
    {
    }

    public AeroVelozDbContext(DbContextOptions<AeroVelozDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Airline> Airlines { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Audit> Audits { get; set; }

    public virtual DbSet<AuditType> AuditTypes { get; set; }

    public virtual DbSet<ChannelSubscriptionNotification> ChannelSubscriptionNotifications { get; set; }

    public virtual DbSet<ConectionsAirlineAirport> ConectionsAirlineAirports { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightHistory> FlightHistories { get; set; }

    public virtual DbSet<FlightState> FlightStates { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<OperationChange> OperationChanges { get; set; }

    public virtual DbSet<OperationalChangeType> OperationalChangeTypes { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<ProviderResponse> ProviderResponses { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolPermission> RolPermissions { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserOrganizaton> UserOrganizatons { get; set; }
 

  protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airline>(entity =>
        {
            entity.HasKey(e => e.CodeAirlines).HasName("PK__Airlines__2AA33E8FDD217BC8");

            entity.ToTable("Airlines", "Flights");

            entity.Property(e => e.CodeAirlines)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codeAirlines");
            entity.Property(e => e.CodeIata)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codeIATA");
            entity.Property(e => e.IdOrganization).HasColumnName("idOrganization");

            entity.HasOne(d => d.IdOrganizationNavigation).WithMany(p => p.Airlines)
                .HasForeignKey(d => d.IdOrganization)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Organization_Airport");
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.CodeAirport).HasName("PK__Airports__ADE75BD28294035A");

            entity.ToTable("Airports", "Airport");

            entity.Property(e => e.CodeAirport)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codeAirport");
            entity.Property(e => e.ApiKeyMaster)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Api_Key_master");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.CodeIata)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codeIATA");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.IdOrganization).HasColumnName("idOrganization");
            entity.Property(e => e.TimeZone).HasColumnName("timeZone");

            entity.HasOne(d => d.IdOrganizationNavigation).WithMany(p => p.Airports)
                .HasForeignKey(d => d.IdOrganization)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Organization_Airport");
        });

        modelBuilder.Entity<Audit>(entity =>
        {
            entity.HasKey(e => e.IdAuditEntry).HasName("PK__Audit__B7FA15769E2FFD22");

            entity.ToTable("Audit", "Audits", tb => tb.HasTrigger("trg_audit"));

            entity.Property(e => e.IdAuditEntry)
                .ValueGeneratedNever()
                .HasColumnName("idAuditEntry");
            entity.Property(e => e.DataNew).HasColumnType("json");
            entity.Property(e => e.DataOld).HasColumnType("json");
            entity.Property(e => e.IdAuditType).HasColumnName("idAuditType");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.NameEntity)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nameEntity");
            entity.Property(e => e.OcurrentAt)
                .HasColumnType("datetime")
                .HasColumnName("ocurrentAt");

            entity.HasOne(d => d.IdAuditTypeNavigation).WithMany(p => p.Audits)
                .HasForeignKey(d => d.IdAuditType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auditType");
        });

        modelBuilder.Entity<AuditType>(entity =>
        {
            entity.HasKey(e => e.IdAuditType).HasName("PK__AuditTyp__3DBB345B6D49B452");

            entity.ToTable("AuditType", "Audits");

            entity.Property(e => e.IdAuditType)
                .ValueGeneratedNever()
                .HasColumnName("idAuditType");
            entity.Property(e => e.NameAudit)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nameAudit");
        });

        modelBuilder.Entity<ChannelSubscriptionNotification>(entity =>
        {
            entity.HasKey(e => e.CodeChannel).HasName("PK__ChannelS__B3E56961E7367229");

            entity.ToTable("ChannelSubscriptionNotification", "Subscriptions");

            entity.Property(e => e.CodeChannel).HasColumnName("codeChannel");
            entity.Property(e => e.Name)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("name");
        });

        modelBuilder.Entity<ConectionsAirlineAirport>(entity =>
        {
            entity.HasKey(e => e.IdConection).HasName("PK__Conectio__44E6975FB585D55D");

            entity.ToTable("ConectionsAirlineAirport", "Airport");

            entity.Property(e => e.IdConection)
                .ValueGeneratedNever()
                .HasColumnName("idConection");
            entity.Property(e => e.CodeAirlines)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codeAirlines");
            entity.Property(e => e.CodeAirport)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codeAirport");
            entity.Property(e => e.TokenApi)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tokenApi");

            entity.HasOne(d => d.CodeAirlinesNavigation).WithMany(p => p.ConectionsAirlineAirports)
                .HasForeignKey(d => d.CodeAirlines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_AirlinesConection");

            entity.HasOne(d => d.CodeAirportNavigation).WithMany(p => p.ConectionsAirlineAirports)
                .HasForeignKey(d => d.CodeAirport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_AirportConection");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightNumber).HasName("PK__Flight__4E642B65CC61E67D");

            entity.ToTable("Flight", "Flights", tb => tb.HasTrigger("trg_FlightStateChange"));

            entity.Property(e => e.FlightNumber)
                .ValueGeneratedNever()
                .HasColumnName("flightNumber");
            entity.Property(e => e.BoardingGateArrived)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.BordingGate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CodeAirlines)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codeAirlines");
            entity.Property(e => e.DestinationAirport)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FlightStatesId).HasColumnName("flightStatesId");
            entity.Property(e => e.OriginAirport)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.CodeAirlinesNavigation).WithMany(p => p.Flights)
                .HasForeignKey(d => d.CodeAirlines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Airlines");

            entity.HasOne(d => d.DestinationAirportNavigation).WithMany(p => p.FlightDestinationAirportNavigations)
                .HasForeignKey(d => d.DestinationAirport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DestinationAirport");

            entity.HasOne(d => d.FlightStates).WithMany(p => p.Flights)
                .HasForeignKey(d => d.FlightStatesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_State");

            entity.HasOne(d => d.OriginAirportNavigation).WithMany(p => p.FlightOriginAirportNavigations)
                .HasForeignKey(d => d.OriginAirport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OriginAirport");
        });

        modelBuilder.Entity<FlightHistory>(entity =>
        {
            entity.HasKey(e => new { e.FlightNumber, e.CodeAirlines }).HasName("PK__FlightHi__ACCE188DA97A7E6D");

            entity.ToTable("FlightHistory", "Flights");

            entity.Property(e => e.FlightNumber).HasColumnName("flightNumber");
            entity.Property(e => e.CodeAirlines)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codeAirlines");
            entity.Property(e => e.ChangeAt)
                .HasColumnType("datetime")
                .HasColumnName("changeAt");
            entity.Property(e => e.FlightStatedsIdBefore).HasColumnName("flightStatedsIdBefore");
            entity.Property(e => e.FlightStatesIdAfter).HasColumnName("flightStatesIdAfter");
            entity.Property(e => e.Reason)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("reason");

            entity.HasOne(d => d.CodeAirlinesNavigation).WithMany(p => p.FlightHistories)
                .HasForeignKey(d => d.CodeAirlines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Flight_HistoryA");

            entity.HasOne(d => d.FlightNumberNavigation).WithMany(p => p.FlightHistories)
                .HasForeignKey(d => d.FlightNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Flight_HistoryN");

            entity.HasOne(d => d.FlightStatedsIdBeforeNavigation).WithMany(p => p.FlightHistoryFlightStatedsIdBeforeNavigations)
                .HasForeignKey(d => d.FlightStatedsIdBefore)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Flight_History_State_Before");

            entity.HasOne(d => d.FlightStatesIdAfterNavigation).WithMany(p => p.FlightHistoryFlightStatesIdAfterNavigations)
                .HasForeignKey(d => d.FlightStatesIdAfter)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Flight_History_State_After");
        });

        modelBuilder.Entity<FlightState>(entity =>
        {
            entity.HasKey(e => e.FlightStatesId).HasName("PK__FlightSt__2FFF91110EDF67E8");

            entity.ToTable("FlightStates", "Flights");

            entity.HasIndex(e => e.CodeFlightState, "UQ__FlightSt__D221ACD86146929D").IsUnique();

            entity.Property(e => e.FlightStatesId).HasColumnName("flightStatesId");
            entity.Property(e => e.CodeFlightState)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codeFlightState");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationsId).HasName("PK__Notifica__8CBD3CC77EC3E2C4");

            entity.ToTable("Notification", "Notifications");

            entity.Property(e => e.NotificationsId)
                .ValueGeneratedNever()
                .HasColumnName("notificationsId");
            entity.Property(e => e.CodeProvider).HasColumnName("codeProvider");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("createAt");
            entity.Property(e => e.Message)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.StatusNotification).HasColumnName("statusNotification");
            entity.Property(e => e.SubscripcionId).HasColumnName("subscripcionId");

            entity.HasOne(d => d.CodeProviderNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.CodeProvider)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_notification_provider");

            entity.HasOne(d => d.Subscripcion).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.SubscripcionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_subscription");
        });

        modelBuilder.Entity<OperationChange>(entity =>
        {
            entity.HasKey(e => e.OperationId).HasName("PK__Operatio__34C2D1D9D2174074");

            entity.ToTable("OperationChange", "Operations");

            entity.Property(e => e.OperationId)
                .ValueGeneratedNever()
                .HasColumnName("operationId");
            entity.Property(e => e.Cause)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("cause");
            entity.Property(e => e.ChangeAt)
                .HasColumnType("datetime")
                .HasColumnName("changeAt");
            entity.Property(e => e.CodeAirline)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codeAirline");
            entity.Property(e => e.FlighNumber).HasColumnName("flighNumber");
            entity.Property(e => e.IdOperationalType).HasColumnName("idOperationalType");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.NewValue)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("newValue");
            entity.Property(e => e.PrivousValue)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("privousValue");

            entity.HasOne(d => d.CodeAirlineNavigation).WithMany(p => p.OperationChanges)
                .HasForeignKey(d => d.CodeAirline)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_flight_airline2");

            entity.HasOne(d => d.FlighNumberNavigation).WithMany(p => p.OperationChanges)
                .HasForeignKey(d => d.FlighNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_flight_airline");

            entity.HasOne(d => d.IdOperationalTypeNavigation).WithMany(p => p.OperationChanges)
                .HasForeignKey(d => d.IdOperationalType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_operationalType");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.OperationChanges)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<OperationalChangeType>(entity =>
        {
            entity.HasKey(e => e.IdOperationalType).HasName("PK__Operatio__CFA2B462CA149EB5");

            entity.ToTable("OperationalChangeType", "Operations");

            entity.Property(e => e.IdOperationalType)
                .ValueGeneratedNever()
                .HasColumnName("idOperationalType");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.IdOrganizations).HasName("PK__Organiza__5EA758FDA4B54F5D");

            entity.ToTable("Organizations", "Identitys");

            entity.HasIndex(e => e.EmailOrganizations, "UQ__Organiza__8F9312CE4EA88394").IsUnique();

            entity.Property(e => e.IdOrganizations).HasColumnName("idOrganizations");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createAt");
            entity.Property(e => e.EmailOrganizations)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("emailOrganizations");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.NameOrganization)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nameOrganization");
            entity.Property(e => e.TypeOrganization)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("typeOrganization");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.IdPermission).HasName("PK__Permissi__A08B06819E851162");

            entity.ToTable("Permissions", "Identitys");

            entity.Property(e => e.IdPermission)
                .ValueGeneratedNever()
                .HasColumnName("idPermission");
            entity.Property(e => e.CodePermission)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codePermission");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProviderResponse>(entity =>
        {
            entity.HasKey(e => e.CodeProvider).HasName("PK__Provider__11C8A1B20A1B6EE2");

            entity.ToTable("ProviderResponse", "Notifications");

            entity.Property(e => e.CodeProvider)
                .ValueGeneratedNever()
                .HasColumnName("codeProvider");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F7605E95E76");

            entity.ToTable("Rol", "Identitys");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("idRol");
            entity.Property(e => e.NameRol)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nameRol");
        });

        modelBuilder.Entity<RolPermission>(entity =>
        {
            entity.HasKey(e => e.IdRolPermission).HasName("PK__RolPermi__02914F8C8A9A458D");

            entity.ToTable("RolPermissions", "Identitys");

            entity.Property(e => e.IdRolPermission).HasColumnName("idRolPermission");
            entity.Property(e => e.IdPermission).HasColumnName("idPermission");
            entity.Property(e => e.IdRol).HasColumnName("idRol");

            entity.HasOne(d => d.IdPermissionNavigation).WithMany(p => p.RolPermissions)
                .HasForeignKey(d => d.IdPermission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Rol_Permission_Permission");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolPermissions)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Rol_Permission_Rol");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscripcionId).HasName("PK__Subscrip__E9CB8665BB7DFB8A");

            entity.ToTable("Subscription", "Subscriptions");

            entity.Property(e => e.SubscripcionId)
                .ValueGeneratedNever()
                .HasColumnName("subscripcionId");
            entity.Property(e => e.ActiveSubscription).HasColumnName("activeSubscription");
            entity.Property(e => e.CodeAirlines)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codeAirlines");
            entity.Property(e => e.CodeChannel).HasColumnName("codeChannel");
            entity.Property(e => e.ContactValue)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("contactValue");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("createDate");
            entity.Property(e => e.EndingDate)
                .HasColumnType("datetime")
                .HasColumnName("endingDate");
            entity.Property(e => e.FlightNumber).HasColumnName("flightNumber");
            entity.Property(e => e.NumberInterested).HasColumnName("numberInterested");

            entity.HasOne(d => d.CodeAirlinesNavigation).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.CodeAirlines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_subscriptionA");

            entity.HasOne(d => d.CodeChannelNavigation).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.CodeChannel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_subscription_Code_Channel");

            entity.HasOne(d => d.FlightNumberNavigation).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.FlightNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_subscriptionF");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__3717C9822C2122CA");

            entity.ToTable("Users", "Identitys");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("idUser");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createAt");
            entity.Property(e => e.FailedLoginAttempts)
                .HasDefaultValue(0)
                .HasColumnName("failedLoginAttempts");
            entity.Property(e => e.IpAdress)
                .HasMaxLength(16)
                .HasColumnName("ipAdress");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.LastLoginAt)
                .HasColumnType("datetime")
                .HasColumnName("lastLoginAt");
            entity.Property(e => e.LockedUntil)
                .HasColumnType("datetime")
                .HasColumnName("lockedUntil");
            entity.Property(e => e.NameUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nameUser");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("passwordHash");
        });

        modelBuilder.Entity<UserOrganizaton>(entity =>
        {
            entity.HasKey(e => e.IdUserOrganization).HasName("PK__UserOrga__41EB21B7706EDD0B");

            entity.ToTable("UserOrganizatons", "Identitys");

            entity.Property(e => e.IdUserOrganization).HasColumnName("idUserOrganization");
            entity.Property(e => e.IdOrganizations).HasColumnName("idOrganizations");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.IdUser).HasColumnName("idUser");

            entity.HasOne(d => d.IdOrganizationsNavigation).WithMany(p => p.UserOrganizatons)
                .HasForeignKey(d => d.IdOrganizations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Organization");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.UserOrganizatons)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Rol");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserOrganizatons)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
