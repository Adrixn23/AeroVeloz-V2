//using MediatR;

namespace AeroVeloz.Domain.Events.Operations.SecurityOperationEvents
{
    public record DomesticFlightCancellationEvent(

         int flightNumber,
         string? codeAirline,
         string? codeAirport,
         int idUserOperational,
         string? cause,
         DateTime CancellationDecisionTime
        //FlightState stateBefore

        );
        //) : INotification;
    
}
