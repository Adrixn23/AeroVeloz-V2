//using MediatR;

namespace AeroVeloz.Domain.Events.Operations.AircraftManagementEvents
{
    public record FlightLandedEvent(

        int FlightNumber,
        string? codeAirportArrival,
        //FlightState stateBefore,
        DateTimeOffset ScheduledArrival,
        string? codeAirline,
        string? codeAirportOrigin,
        string? GateDeparture
        //Flight stateAfter,
        );
        //) : INotification;
    
}
