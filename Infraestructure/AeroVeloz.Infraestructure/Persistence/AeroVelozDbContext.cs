using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Entities.Airports;
using Microsoft.EntityFrameworkCore;

using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Entities.Flights;
using AeroVeloz.Domain.Entities.Subscriptions;

using System.Reflection;

namespace AeroVeloz.Infrastructure.Persistence
{
    public class AeroVelozDbContext : DbContext
    {
        public AeroVelozDbContext(DbContextOptions<AeroVelozDbContext> options) : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightState> FlightStates { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<FlightHistory> FlightHistories { get; set; }
         
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}