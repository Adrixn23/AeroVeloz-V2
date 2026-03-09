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
<<<<<<< HEAD
                new FlightState { Id = 1, code = "SCH", StateName = "Scheduled" },
                new FlightState { Id = 2, code = "PRO", StateName = "InProcess" },
                new FlightState { Id = 3, code = "DEL", StateName = "Delayed" },
                new FlightState { Id = 4, code = "INF", StateName = "InFlight" },
                new FlightState { Id = 5, code = "ARR", StateName = "LandedArrived" },
                new FlightState { Id = 6, code = "FIN", StateName = "Completed" },
                new FlightState { Id = 7, code = "CAN", StateName = "Cancelled" },
                new FlightState { Id = 8, code = "DIV", StateName = "Diverted" }
           );
       }
=======
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
>>>>>>> origin/testeo-branch
    }
}
