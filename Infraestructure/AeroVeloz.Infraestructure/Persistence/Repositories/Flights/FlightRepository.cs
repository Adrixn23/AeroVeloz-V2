using AeroVeloz.Application.DTOs.Flights;
using AeroVeloz.Application.Repositories.Flights;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace AeroVeloz.Infraestructure.Persistence.Repositories.Flights
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AeroVelozContext _context;
        private readonly ILogger<FlightRepository> _logger;

        public FlightRepository(AeroVelozContext context, ILogger<FlightRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<FlightListDto>> GetActiveFlightsForAirportAsync(string airportCode)
        {
            try
            {
                var query = _context.Flights.AsNoTracking()
                    .Where(f => f.OriginAirport == airportCode || f.DestinationAirport == airportCode)

                    .OrderByDescending(f => f.ScheduledDeparture);

                var flights = await query.Select(f => new FlightListDto
                {
                    Id = f.Id,
                    CodeAirlineIcao = f.codeAirlinesIcao,
                    FlightNumber = f.Id.ToString(), 
                    OriginAirport = f.OriginAirport,
                    DestinationAirport = f.DestinationAirport,
                    ScheduledDeparture = f.ScheduledDeparture,
                    BordingGate = f.BordingGate,
                    FlightStateId = f.flightStatesId
                }).ToListAsync();

                return flights;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching active flights for airport {AirportCode}", airportCode);
                return Array.Empty<FlightListDto>();
            }
        }

        public async Task<IReadOnlyCollection<FlightDetailsDto>> GetAllActiveFlightsWithDetailsAsync()
        {
            try
            {
                var flights = await (
                    from f in _context.Flights.AsNoTracking()
                    join fs in _context.FlightStates.AsNoTracking() on f.flightStatesId equals fs.Id
                    select new FlightDetailsDto
                    {
                        Id = f.Id,
                        CodeAirlineIcao = f.codeAirlinesIcao,
                        FlightNumber = f.Id.ToString(),
                        OriginAirport = f.OriginAirport,
                        DestinationAirport = f.DestinationAirport,
                        ScheduledDeparture = f.ScheduledDeparture,
                        BordingGate = f.BordingGate,
                        BoardingGateArrived = f.BoardingGateArrived,
                        FlightStateId = f.flightStatesId,
                        FlightStateName = fs.name,
                        TotalOperations = _context.OperationChanges.AsNoTracking()
                            .Count(op => op.flightNumber == f.Id),
                        ActiveOperations = _context.OperationChanges.AsNoTracking()
                            .Count(op => op.flightNumber == f.Id && op.isActive)
                    }
                ).OrderByDescending(f => f.ScheduledDeparture)
                 .ToListAsync();

                return flights;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all active flights with details");
                return Array.Empty<FlightDetailsDto>();
            }
        }

        public async Task<IReadOnlyCollection<FlightDetailsDto>> GetFlightsByAirportWithDetailsAsync(string airportCode)
        {
            try
            {
                var flights = await (
                    from f in _context.Flights.AsNoTracking()
                        .Where(f => f.OriginAirport == airportCode || f.DestinationAirport == airportCode)
                    join fs in _context.FlightStates.AsNoTracking() on f.flightStatesId equals fs.Id
                    select new FlightDetailsDto
                    {
                        Id = f.Id,
                        CodeAirlineIcao = f.codeAirlinesIcao,
                        FlightNumber = f.Id.ToString(),
                        OriginAirport = f.OriginAirport,
                        DestinationAirport = f.DestinationAirport,
                        ScheduledDeparture = f.ScheduledDeparture,
                        BordingGate = f.BordingGate,
                        BoardingGateArrived = f.BoardingGateArrived,
                        FlightStateId = f.flightStatesId,
                        FlightStateName = fs.name,
                        TotalOperations = _context.OperationChanges.AsNoTracking()
                            .Count(op => op.flightNumber == f.Id),
                        ActiveOperations = _context.OperationChanges.AsNoTracking()
                            .Count(op => op.flightNumber == f.Id && op.isActive)
                    }
                ).OrderByDescending(f => f.ScheduledDeparture)
                 .ToListAsync();

                return flights;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching flights by airport {AirportCode} with details", airportCode);
                return Array.Empty<FlightDetailsDto>();
            }
        }

        public async Task<FlightDetailsDto?> GetFlightWithDetailsAsync(short flightId)
        {
            try
            {
                var flight = await (
                    from f in _context.Flights.AsNoTracking()
                        .Where(f => f.Id == flightId)
                    join fs in _context.FlightStates.AsNoTracking() on f.flightStatesId equals fs.Id
                    select new FlightDetailsDto
                    {
                        Id = f.Id,
                        CodeAirlineIcao = f.codeAirlinesIcao,
                        FlightNumber = f.Id.ToString(),
                        OriginAirport = f.OriginAirport,
                        DestinationAirport = f.DestinationAirport,
                        ScheduledDeparture = f.ScheduledDeparture,
                        BordingGate = f.BordingGate,
                        BoardingGateArrived = f.BoardingGateArrived,
                        FlightStateId = f.flightStatesId,
                        FlightStateName = fs.name,
                        TotalOperations = _context.OperationChanges.AsNoTracking()
                            .Count(op => op.flightNumber == f.Id),
                        ActiveOperations = _context.OperationChanges.AsNoTracking()
                            .Count(op => op.flightNumber == f.Id && op.isActive)
                    }
                ).FirstOrDefaultAsync();

                return flight;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching flight {FlightId} with details", flightId);
                return null;
            }
        }
    }
}
