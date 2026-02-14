using System;
using System.Collections.Generic;
using AeroVeloz.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Entities;

public partial class AeroVelozContext : DbContext
{
    public AeroVelozContext()
    {
    }

    public AeroVelozContext(DbContextOptions<AeroVelozContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Airline> Airlines { get; set; }

    public virtual DbSet<AuditEntry> AuditEntries { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightHistory> FlightHistories { get; set; }

    public virtual DbSet<FlightState> FlightStates { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<OperationChange> OperationChanges { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<ViewActiveSubscription> ViewActiveSubscriptions { get; set; }

    public virtual DbSet<ViewFlightHistoryChronological> ViewFlightHistoryChronologicals { get; set; }

    public virtual DbSet<ViewPublicFlights48h> ViewPublicFlights48hs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airline>(entity =>
        {
            entity.HasKey(e => e.AirlineId).HasName("PK__Airlines__DC458213A7562452");

            entity.Property(e => e.AirlineId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AirlineCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.AirlineName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AuditEntry>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__AuditEnt__A17F2398BBC290CC");

            entity.ToTable(tb => tb.HasTrigger("trg_Audit_Immutable"));

            entity.Property(e => e.AuditId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ActionType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChangeDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Details).IsUnicode(false);
            entity.Property(e => e.RecordId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TableName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flights__8A9E148E5E4BDE78");

            entity.ToTable(tb => tb.HasTrigger("trg_FlightStateChange"));

            entity.Property(e => e.FlightId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("FlightID");
            entity.Property(e => e.Destination)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FlightNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Origin)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ScheduledTime).HasColumnType("datetime");
            entity.Property(e => e.StateId).HasColumnName("StateID");

            entity.HasOne(d => d.Airline).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AirlineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Flights__Airline__30C33EC3");

            entity.HasOne(d => d.State).WithMany(p => p.Flights)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Flights__StateID__31B762FC");
        });

        modelBuilder.Entity<FlightHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__FlightHi__4D7B4ABDAFE4FF55");

            entity.ToTable("FlightHistory");

            entity.Property(e => e.HistoryId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ChangeDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Flight).WithMany(p => p.FlightHistories)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FlightHis__Fligh__3A4CA8FD");
        });

        modelBuilder.Entity<FlightState>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__FlightSt__C3BA3B5ABF9ED1E5");

            entity.Property(e => e.StateId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("StateID");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.StateName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E1258D92685");

            entity.Property(e => e.NotificationId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Message)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ProviderResponse)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TypeNotification)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Subscription).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__Subsc__4D5F7D71");
        });

        modelBuilder.Entity<OperationChange>(entity =>
        {
            entity.HasKey(e => e.OperationId).HasName("PK__Operatio__A4F5FC44EA0A3291");

            entity.ToTable("OperationChange");

            entity.Property(e => e.OperationId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Cause)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ChangeAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NewValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PreviousValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TypeChange)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ActorRefNavigation).WithMany(p => p.OperationChanges)
                .HasForeignKey(d => d.ActorRef)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Operation__Actor__489AC854");

            entity.HasOne(d => d.Flight).WithMany(p => p.OperationChanges)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Operation__Fligh__47A6A41B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__roles__8AFACE1ACF94C902");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__9A2B249D1C5EADB9");

            entity.Property(e => e.SubscriptionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");

            entity.HasOne(d => d.Flight).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subscript__Fligh__3F115E1A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__1788CC4CC51CE831");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAT");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__RoleI__3587F3E0"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__UserI__3493CFA7"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760AD669164B8");
                        j.ToTable("UserRoles");
                    });
        });

        modelBuilder.Entity<ViewActiveSubscription>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_ActiveSubscriptions");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FlightNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewFlightHistoryChronological>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_FlightHistory_Chronological");

            entity.Property(e => e.ChangeDate).HasColumnType("datetime");
            entity.Property(e => e.FlightNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NewState)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OldState)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewPublicFlights48h>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_PublicFlights_48h");

            entity.Property(e => e.AirlineName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Destination)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FlightNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Origin)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ScheduledTime).HasColumnType("datetime");
            entity.Property(e => e.StateName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
