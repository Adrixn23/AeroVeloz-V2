using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.Events.EventsAirlines
{
    public record FlightBatchProcessed(
        string AirlineCode,
        int TotalFlightProcessedAT,
        DateTime ProcessedAT

        ) : INotification;
    
    
}
