using AeroVeloz.Domain.Entities.Audit;
using AeroVeloz.Domain.Entities.Flights;
using AeroVeloz.Domain.Entities.Notification;
using AeroVeloz.Domain.Entities.Organization.Airport;
using AeroVeloz.Domain.Entities.Organization.Airports;
using AeroVeloz.Domain.Entities.Organization.Base;
using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Entities.Subscriptions;
using AeroVeloz.Domain.Entities.Users.Permission;
using AeroVeloz.Domain.Entities.Users.Roles;
using AeroVeloz.Domain.Entities.Users.RolesPermision;
using AeroVeloz.Domain.Entities.Users.User;
using AeroVeloz.Domain.Entities.Operations;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Configurations
{
    public static class DatabaseSchemaSyncConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            // Identitys Schema
            modelBuilder.Entity<Organizations>().ToTable("Organizations", "Identitys");
            modelBuilder.Entity<Organizations>().Property(x => x.Id).UseIdentityColumn(); // Único con Identity

            modelBuilder.Entity<User>().ToTable("Users", "Identitys");
            
            modelBuilder.Entity<Roles>().ToTable("Rol", "Identitys");
            modelBuilder.Entity<Roles>().Property(x => x.Id).ValueGeneratedNever(); // Sin Identity según SQL

            modelBuilder.Entity<Permissions>().ToTable("Permissions", "Identitys");
            modelBuilder.Entity<Permissions>().Property(x => x.Id).ValueGeneratedNever(); // Sin Identity según SQL

            modelBuilder.Entity<RolPermission>().ToTable("RolPermissions", "Identitys");
            modelBuilder.Entity<RolPermission>().Property(x => x.idRolPermission).ValueGeneratedNever();

            // Airport Schema
            modelBuilder.Entity<Airport>().ToTable("Airports", "Airport");
            modelBuilder.Entity<ConectionsAirlineAirport>().ToTable("ConectionsAirlineAirport", "Airport");

            // Flights Schema
            modelBuilder.Entity<Airline>().ToTable("Airlines", "Flights");
            modelBuilder.Entity<FlightState>().ToTable("FlightStates", "Flights");
            modelBuilder.Entity<FlightState>().Property(x => x.Id).ValueGeneratedNever(); // Sin Identity según SQL

            modelBuilder.Entity<Flight>().ToTable("Flight", "Flights");

            modelBuilder.Entity<FlightHistory>().ToTable("FlightHistory", "Flights")
                .HasKey(fh => new { fh.flightNumber, fh.codeAirlines, fh.changeAt });

            // Subscriptions Schema
            modelBuilder.Entity<ChannelSubscriptionNotification>().ToTable("ChannelSubscriptionNotification", "Subscriptions");
            modelBuilder.Entity<ChannelSubscriptionNotification>().Property(x => x.Id).ValueGeneratedNever();

            modelBuilder.Entity<Subscription>().ToTable("Subscription", "Subscriptions");

            // Notifications Schema
            modelBuilder.Entity<ProviderResponse>().ToTable("ProviderResponse", "Notifications");
            modelBuilder.Entity<ProviderResponse>().Property(x => x.Id).ValueGeneratedNever();

            modelBuilder.Entity<Notification>().ToTable("Notification", "Notifications");

            // Audits Schema
            modelBuilder.Entity<AuditType>().ToTable("AuditType", "Audits");
            modelBuilder.Entity<AuditType>().Property(x => x.idAuditType).ValueGeneratedNever();

            modelBuilder.Entity<Audit>().ToTable("Audit", "Audits");

            // Operations Schema
            modelBuilder.Entity<OperationalChangeType>().ToTable("OperationalChangeType", "Operations");
            modelBuilder.Entity<OperationalChangeType>().Property(x => x.Id).ValueGeneratedNever();

            modelBuilder.Entity<OperationChange>().ToTable("OperationChange", "Operations");
        }
    }
}
