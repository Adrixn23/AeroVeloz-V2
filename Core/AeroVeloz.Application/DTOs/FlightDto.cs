using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroVeloz.Application.DTOs
{
    public record FlightCreateDto(
        string AirlineCode,
        string FlightNumber, // El código de negocio (ej: AV204)
        string OriginAirport,
        string DestinationAirport,
        DateTime ScheduledDeparture,
        DateTime ScheduledArrival,
        byte InitialStateId
    );

    public record FlightResponseDto(
    short Id,
    string FlightNumber,
    string AirlineName,
    string OriginAirport,
    string DestinationAirport,
    string StatusName,
    DateTime DepartureTime,
    bool IsOperational // muy util para filtrar la regla de las 48 horass
);
}
public record FlightUpdateStatusDto(
    short FlightId,
    byte NewStateId,
    string AuthorizedAirlineCode
);
public record BatchResponseDto(
    int ProcessedCount,
    int ErrorCount,
    List<string> Messages
);
