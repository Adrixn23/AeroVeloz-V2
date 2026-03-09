

using AeroVeloz.Application.Repositories.Flights;
using AeroVeloz.Domain.Models.Flights;

using AeroVeloz.Infraestructure.Persistence.context;
using AeroVeloz.Domain.DomainService.Interfaces.Flight;
using AeroVeloz.Domain.Common.Validation;
using AeroVeloz.Domain.Entities.Flights;
using AeroVeloz.Domain.Common.Enums;
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



        

        public Task<bool> IsOrganizationActiveAsync(int idOrganization)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsOriginAirportActiveAsync(string airportCode)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> IsValidDestinationAirportAsync(string airportCode)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> IsValidOriginAirportAsync(string airportCode)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> IsValidStatusTransitionAsync(Flight flight, FlightStateEnum newStatus)
        {
            throw new NotImplementedException();
        }

    
        public Task<short> GetFlightIdNumberAsync(string airlineCode)
        {
            throw new NotImplementedException();
        }


        public Task<FlightReadModel?> GetByFlightAndAirlineAsync(short flightNumber, string iataCode)
        {
            throw new NotImplementedException();
        }
    }
}