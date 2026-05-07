using AeroVeloz.Application.DTOs.Flights;

namespace AeroVeloz.Application.Contracts.Flights
{
    public interface ICsvFlightParser
    {
        IReadOnlyCollection<FlightBatchItemDto> Parse(Stream csvStream, out IReadOnlyCollection<string> parseErrors);
    }
}
