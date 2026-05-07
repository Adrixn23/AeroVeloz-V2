using AeroVeloz.Domain.Entities.Users.User;
using Microsoft.EntityFrameworkCore;
using AeroVeloz.Domain.Entities.Users.Roles;
using AeroVeloz.Domain.Entities.Users.Permission;
using AeroVeloz.Domain.Entities.Organization.Airport;
using AeroVeloz.Domain.Entities.Organization.Base;
using AeroVeloz.Domain.Entities.Flights;
using AeroVeloz.Domain.Entities.Audit;
using AeroVeloz.Domain.Entities.Notification;
using AeroVeloz.Domain.Entities.Subscriptions;
using AeroVeloz.Domain.Entities.Users.RolesPermision;
using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Infraestructure.Persistence.Configurations;

namespace AeroVeloz.Infraestructure.Persistence.context
{
    public class AeroVelozContext : DbContext
    {
        public AeroVelozContext(DbContextOptions<AeroVelozContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<RolPermission> RolPermissions { get; set; }
        public DbSet<Organizations> Organizations { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<ConectionsAirlineAirport> ConectionsAirlineAirports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightHistory> FlightHistory { get; set; }
        public DbSet<FlightState> FlightStates { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<ChannelSubscriptionNotification> ChannelSubscriptionNotifications { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ProviderResponse> ProviderResponses { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<AuditType> AuditTypes { get; set; }
        public DbSet<OperationalChangeType> OperationalChangeTypes { get; set; }
        public DbSet<OperationChange> OperationChanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AeroVelozContext).Assembly);

            DatabaseSchemaSyncConfiguration.Configure(modelBuilder);
        }
    }
}
