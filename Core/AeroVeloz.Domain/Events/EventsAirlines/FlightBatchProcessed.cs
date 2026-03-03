using MediatR;
using System;
    
namespace AeroVeloz.Domain.Events.EventsAirlines
{
    public record FlightBatchProcessed(
        string AirlineCode,
        int TotalFlightProcessedAT,
        DateTime ProcessedAT 

        ) : INotification;
    
    
}
