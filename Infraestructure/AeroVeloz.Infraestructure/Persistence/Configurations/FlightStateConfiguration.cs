using AeroVeloz.Domain.Entities.Flights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroVeloz.Infraestructure.Persistence.Configurations
{
    public class FlightStateConfiguration : IEntityTypeConfiguration<FlightState>
    {
        public void Configure(EntityTypeBuilder<FlightState> builder)
        {
            builder.HasData(
                new FlightState { Id = Guid.Parse("1"), code = "SCH", StateName = "Scheduled" },
                new FlightState { Id = Guid.Parse("2"), code = "PRO", StateName = "InProcess" },
                new FlightState { Id = Guid.Parse("3"), code = "DEL", StateName = "Delayed" },
                new FlightState { Id = Guid.Parse("4"), code = "INF", StateName = "InFlight" },
                new FlightState { Id = Guid.Parse("5"), code = "ARR", StateName = "LandedArrived" },
                new FlightState { Id = Guid.Parse("6"), code = "FIN", StateName = "Completed" },
                new FlightState { Id = Guid.Parse("7"), code = "CAN", StateName = "Cancelled" },
                new FlightState { Id = Guid.Parse("8"), code = "DIV", StateName = "Diverted" }
            );
        }
    }
}
