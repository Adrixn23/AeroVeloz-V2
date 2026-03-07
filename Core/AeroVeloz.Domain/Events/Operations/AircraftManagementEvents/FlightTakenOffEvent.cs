//using MediatR;

namespace AeroVeloz.Domain.Events.Operations.AircraftManagementEvents
{
    public record FlightTakenOffEvent(

        int FlightNumber,
        string? codeAirportDeparture,
        //FlightState stateBefore,
        DateTimeOffset ScheduledArrive,
        string? codeAirline,
        string? codeAirportArrived,
        string? GateApproach
        //Flight stateAfter,
        );
        //) : INotification;
    
}
