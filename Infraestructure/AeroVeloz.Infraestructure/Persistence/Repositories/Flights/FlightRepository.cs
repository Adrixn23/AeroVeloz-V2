using AeroVeloz.Application.DTOs.Flights;
using AeroVeloz.Application.Repositories.Flights;
using AeroVeloz.Domain.Entities.Flights;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Flights
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AeroVelozContext _context;

        public FlightRepository(AeroVelozContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateEntity(Flight entity)
        {
            _context.Flights.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEntity(Flight entity)
        {
            _context.Flights.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEntity(Flight entity)
        {
            _context.Flights.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<FlightReadDto?> GetByFlightNumberAndAirlineAsync(short flightNumber, string codeAirlines)
        {
            return await (
                from f in _context.Flights.AsNoTracking()
                join s in _context.FlightStates.AsNoTracking() on f.flightStateId equals s.Id
                where f.Id == flightNumber && f.codeAirlines == codeAirlines
                select new FlightReadDto(
                    f.Id, f.codeAirlines, f.OriginAirport, f.DestinationAirport,
                    f.ScheduledDeparture, f.BordingGate, f.BoardingGateArrived,
                    f.flightStateId, s.name)
            ).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<FlightReadDto>> GetActiveFlightsByAirlineAsync(string codeAirlines)
        {
            return await (
                from f in _context.Flights.AsNoTracking()
                join s in _context.FlightStates.AsNoTracking() on f.flightStateId equals s.Id
                where f.codeAirlines == codeAirlines
                      && f.flightStateId != 6 && f.flightStateId != 7
                orderby f.ScheduledDeparture
                select new FlightReadDto(
                    f.Id, f.codeAirlines, f.OriginAirport, f.DestinationAirport,
                    f.ScheduledDeparture, f.BordingGate, f.BoardingGateArrived,
                    f.flightStateId, s.name)
            ).ToListAsync();
        }

        public async Task<IReadOnlyCollection<FlightReadDto>> GetPublicActiveFlightsAsync()
        {
            var cutoff = DateTimeOffset.UtcNow.AddHours(-2);
            return await (
                from f in _context.Flights.AsNoTracking()
                join s in _context.FlightStates.AsNoTracking() on f.flightStateId equals s.Id
                where f.ScheduledDeparture >= cutoff
                      && f.flightStateId != 6 && f.flightStateId != 7
                orderby f.ScheduledDeparture
                select new FlightReadDto(
                    f.Id, f.codeAirlines, f.OriginAirport, f.DestinationAirport,
                    f.ScheduledDeparture, f.BordingGate, f.BoardingGateArrived,
                    f.flightStateId, s.name)
            ).ToListAsync();
        }

        public async Task<IReadOnlyCollection<FlightReadDto>> GetPublicFlightsByAirportAsync(string airportCode)
        {
            var cutoff = DateTimeOffset.UtcNow.AddHours(-2);
            return await (
                from f in _context.Flights.AsNoTracking()
                join s in _context.FlightStates.AsNoTracking() on f.flightStateId equals s.Id
                where (f.OriginAirport == airportCode || f.DestinationAirport == airportCode)
                      && f.ScheduledDeparture >= cutoff
                      && f.flightStateId != 6 && f.flightStateId != 7
                orderby f.ScheduledDeparture
                select new FlightReadDto(
                    f.Id, f.codeAirlines, f.OriginAirport, f.DestinationAirport,
                    f.ScheduledDeparture, f.BordingGate, f.BoardingGateArrived,
                    f.flightStateId, s.name)
            ).ToListAsync();
        }

        public async Task<bool> ExistsFlightAsync(short flightNumber, string codeAirlines)
        {
            return await _context.Flights.AsNoTracking()
                .AnyAsync(f => f.Id == flightNumber && f.codeAirlines == codeAirlines);
        }

        public async Task<Flight?> GetEntityByNumberAndAirlineAsync(short flightNumber, string codeAirlines)
        {
            return await _context.Flights
                .FirstOrDefaultAsync(f => f.Id == flightNumber && f.codeAirlines == codeAirlines);
        }

        public async Task<bool> PersistBatchAsync(IEnumerable<Flight> flights)
        {
            _context.Flights.AddRange(flights);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateFlightStateAsync(short flightNumber, string codeAirlines, byte newStateId)
        {
            var rows = await _context.Flights
                .Where(f => f.Id == flightNumber && f.codeAirlines == codeAirlines)
                .ExecuteUpdateAsync(s => s.SetProperty(f => f.flightStateId, newStateId));
            return rows > 0;
        }

        public async Task<bool> HasActiveConnectionAsync(string codeAirlines, string airportCode)
        {
            return await _context.ConectionsAirlineAirports.AsNoTracking()
                .AnyAsync(c => c.codeAirlines == codeAirlines && c.codeAirport == airportCode && c.isActive);
        }
    }
}