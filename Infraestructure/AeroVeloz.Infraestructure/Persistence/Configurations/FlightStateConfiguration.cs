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
                new FlightState { Id = 1, codeFlightState = "SCHEDULED",  name = "Scheduled" },
                new FlightState { Id = 2, codeFlightState = "BOARDING",   name = "Boarding" },
                new FlightState { Id = 3, codeFlightState = "DELAYED",    name = "Delayed" },
                new FlightState { Id = 4, codeFlightState = "INFLIGHT",   name = "In Flight" },
                new FlightState { Id = 5, codeFlightState = "LANDED",     name = "Landed" },
                new FlightState { Id = 6, codeFlightState = "COMPLETED",  name = "Completed" },
                new FlightState { Id = 7, codeFlightState = "CANCELLED",  name = "Cancelled" },
                new FlightState { Id = 8, codeFlightState = "DIVERTED",   name = "Diverted" }
            );
        }
    }
}
