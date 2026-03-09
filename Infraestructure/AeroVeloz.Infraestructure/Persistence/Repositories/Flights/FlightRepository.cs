

using AeroVeloz.Application.Repositories.Flights;
using AeroVeloz.Domain.Models.Flights;

using AeroVeloz.Infraestructure.Persistence.context;
using AeroVeloz.Domain.DomainService.Interfaces.Flight;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Flights;
using Microsoft.EntityFrameworkCore;
using AeroVeloz.Domain.Entities.Airlines;
using System.Threading.Tasks.Dataflow;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.Flights
{
    public class FlightRepository : IFlightRepository, IFlightDomainService // tanto la interfaz de iflightrepository y iflightdomainService,
                                                                              
    {
        private readonly AeroVelozContext _context;

        public FlightRepository(AeroVelozContext context)
        {
            _context = context;
        }

      


        public async Task<bool> CreateEntity(Domain.Entities.Flights.Flight entity)
        {
             _context.Flights.Add(entity);
             var result = await _context.SaveChangesAsync();
            return result > 0;

        }

        public async Task<bool> UpdateEntity(Domain.Entities.Flights.Flight entity)
        {
            var result = await _context.Flights
                        .Where(f => f.Id == entity.Id)
                    .ExecuteUpdateAsync(setters => setters
               // Actualizamos el estado del vuelo
            .SetProperty(f => f.flightStateId, entity.flightStateId) // Actualizamos los cambios operativos (Puertas)
            .SetProperty(f => f.BordingGate, entity.BordingGate)
            .SetProperty(f => f.BoardingGateArrived, entity.BoardingGateArrived)
            .SetProperty(f => f.ScheduledDeparture, entity.ScheduledDeparture)

            // Actualizamos horarios por si hubo retrasos autorizados

            );

            return result > 0; // devuelve true si se actualizo 
        }
        public async Task<bool> DeleteEntity(Domain.Entities.Flights.Flight entity)
        {
            _context.Flights.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
            
        }


        public async Task<bool> ExistsFlightAsync(short flightNumber, string airlineCode)
        {
            
           var resultExists = await _context.Flights.AnyAsync(f => f.Id == flightNumber && f.codeAirlines == airlineCode);

            return resultExists;
        }
        public async Task<IReadOnlyCollection<FlightReadModel>> GetActiveFlightsByAirlineAsync(string iataCode)
        {
            var query  =  from airline in _context.Airlines.AsNoTracking()
                        join flight in _context.Flights.AsNoTracking()

                        on airline.codeIATA equals flight.codeAirlines

                        where airline.codeIATA == iataCode

                        select new FlightReadModel
                        (

                            flight.Id,
                            airline.codeIATA!,
                            flight.OriginAirport!,
                            flight.DestinationAirport!,
                            flight.ScheduledDeparture.DateTime,
                            flight.flightStateId,
                            airline.nameOrganization,
                            airline.Id
                        
                        );


                        return await query.ToListAsync();
            // short FlightNumber,
            // string AirlineIataCode,
            //string Origin,
            // string Destination,
            // DateTime DepartureTime,
            //string FlightStatus,
            //string? nameOrganization,
            //int OrgId
        }
        public async Task<bool> IsAirlineOwnerOfFlightAsync(short flightNumber, string airlineCode)
        {
            var exists = await (from airline in _context.Airlines.AsNoTracking()
                                join flight in _context.Flights.AsNoTracking()
                                    on airline.codeIATA equals flight.codeAirlines
                                where flight.Id == flightNumber && airline.codeIATA == airlineCode
                                select flight.Id).AnyAsync();
            return exists;
        }

   public async Task<bool> IsOriginAirportActiveAsync(string airportCode)
        {
            return await _context.Airports
                .Where(a => a.codeAirportIata == airportCode || a.codeAirportIcao == airportCode)
                .Select(a => a.isActived)
                .FirstOrDefaultAsync();
        }

        
        public async Task<ValidationResult> IsValidDestinationAirportAsync(string airportCode)
        {
            var result = new ValidationResult();
            var exists = await _context.Airports
                .AnyAsync(a => a.codeAirportIata == airportCode || a.codeAirportIcao == airportCode);

            if (!exists)
            {
                return result.Failur(ErrosValidationResults.Create("Airport.NotFound", "The destination airport does not exist."));
            }

            return result.Success();
        }

        public async Task<ValidationResult> IsValidOriginAirportAsync(string airportCode)
        {
            var result = new ValidationResult();
            var exists = await _context.Airports
                .AnyAsync(a => (a.codeAirportIata == airportCode || a.codeAirportIcao == airportCode) && a.isActived);

            if (!exists)
            {
                return result.Failur(ErrosValidationResults.Create("Airport.Invalid", "The origin airport is not valid or is inactive."));
            }

            return result.Success();
        }
        public Task<ValidationResult> IsValidStatusTransitionAsync(Flight flight, short newStatus)
        {
            var result = new ValidationResult();
            var currentState = flight.flightStateId;

            // Se impide transicionar desde estados terminales a otros que no sean ellos mismos
            // 6: Finished, 7: Cancelled
            if ((currentState == 7 || currentState == 6) 
                && currentState != newStatus)
            {
                return Task.FromResult(result.Failur(ErrosValidationResults.Create("Flight.InvalidTransition", "Cannot transition from a terminal state.")));
            }

            return Task.FromResult(result.Success());
        }
       
        public async Task<short> GetFlightIdNumberAsync(string airlineCode)
        {
            var maxId = await _context.Flights
                .Where(f => f.codeAirlines == airlineCode)
                .Select(f => (short?)f.Id)
                .MaxAsync() ?? 0;
                
            return (short)(maxId + 1);
        }

        
        public async Task<FlightReadModel?> GetByFlightAndAirlineAsync(short flightNumber, string iataCode)
        {
            var query = from airline in _context.Airlines.AsNoTracking()
                        join flight in _context.Flights.AsNoTracking()
                        on airline.codeIATA equals flight.codeAirlines
                        where flight.Id == flightNumber && airline.codeIATA == iataCode
                        select new FlightReadModel
                        (
                            flight.Id,
                            airline.codeIATA!,
                            flight.OriginAirport!,
                            flight.DestinationAirport!,
                            flight.ScheduledDeparture.DateTime,
                            flight.flightStateId,
                            airline.nameOrganization,
                            airline.Id
                        );

            return await query.FirstOrDefaultAsync();
        }

    }
}