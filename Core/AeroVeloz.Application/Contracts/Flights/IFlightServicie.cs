using AeroVeloz.Application.DTOs.Flights;
using AeroVeloz.Application.Services.Result;

namespace AeroVeloz.Application.Contracts.Flights
{
    public interface IFlightServicie
    {
        Task<OperationResult<FlightBatchResultDto>> UploadBatchAsync(IEnumerable<FlightBatchItemDto> batch, Guid userId, int orgId);
        Task<OperationResult<FlightBatchResultDto>> UploadCsvAsync(Stream csvStream, Guid userId, int orgId, ICsvFlightParser parser);
        Task<OperationResult<bool>> UpdateStateAsync(FlightUpdateStateDto dto, Guid userId, int orgId);
        Task<OperationResult<IReadOnlyCollection<FlightReadDto>>> GetFlightsByAirlineAsync(string codeAirlines, int orgId);
        Task<OperationResult<IReadOnlyCollection<FlightReadDto>>> GetPublicActiveFlightsAsync();
        Task<OperationResult<IReadOnlyCollection<FlightReadDto>>> GetPublicFlightsByAirportAsync(string airportCode);
        Task<OperationResult<FlightReadDto>> GetFlightDetailAsync(short flightNumber, string codeAirlines);
    }
}
