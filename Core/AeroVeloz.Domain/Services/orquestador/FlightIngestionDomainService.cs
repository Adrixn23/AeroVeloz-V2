using AeroVeloz.Domain.DomainService.Interfaces.Airline;
using AeroVeloz.Domain.DomainService.Interfaces.flight;
using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.ValidationBase;
using AeroVeloz.Domain.Validators.codeError.codeError_Airlines; 


namespace AeroVeloz.Domain.DomainService.Flights
{
    public class FlightIngestionDomainService : IFlightIngestionDomainService
    {
        private readonly IAirlineDomainService _airlineDomainService;

        public FlightIngestionDomainService(IAirlineDomainService airlineDomainService)
        {
            _airlineDomainService = airlineDomainService;
        }

        public async Task<ValidationResult> IngestBatchAsync(IEnumerable<Flight> batch, string airlineCode,string currentAirport)
        {
            var result = new ValidationResult();

            // estas son las reglas globales
            var batchValidation = await _airlineDomainService.ProcessFlightBatchAsync(batch, airlineCode, currentAirport);
            if (!batchValidation.IsValid)
            {
                return batchValidation; // aqui falla
            }

            // reglas individuales del vuelo, aqui este servicio nos salva
            foreach (var flight in batch)
            {
                // Error: El tiempo de llegada no puede ser antes o igual al de salida
                if ((flight.ScheduledArrival <= flight.ScheduledDeparture))
                {
                  // aqui se lanza un error flight time del codeError de airlines
                    return result.Failur(ErrorAirlines.InvalidFlightTimeline); 
                }

                // Aquí también podría llamar a _airlineDomainService perono cumple
                
            }

            //entonces este es como el juez que dice que tuvo exito todo, y que tiene sentido 
            return result.Success();  
        }
    }
}