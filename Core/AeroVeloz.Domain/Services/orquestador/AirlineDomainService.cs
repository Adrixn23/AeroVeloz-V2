using AeroVeloz.Domain.Common.Enums;
using AeroVeloz.Domain.DomainService.Interfaces.Airline;
using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesAirline;
using AeroVeloz.Domain.ValidationBase;
using AeroVeloz.Domain.Validators.codeError.codeError_Airlines; // Asegúrate de que el namespace de tus errores coincida
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroVeloz.Domain.DomainService.Airlines
{
    public class AirlineDomainService : IAirlineDomainService
    {
        // Traemos las reglas o politicas que creamos en otra parte
        private readonly IAirlineOwnershipPolicy _ownershipPolicy;
        private readonly IBatchCoherencePolicy _coherencePolicy;

        // Inyectamos las herramientas al arrancar el servicio
        public AirlineDomainService(
            IAirlineOwnershipPolicy ownershipPolicy,
            IBatchCoherencePolicy coherencePolicy)
        {
            _ownershipPolicy = ownershipPolicy;
            _coherencePolicy = coherencePolicy;
        }

       
        //Este es el jefe de la ingesta de vuelos, Revisa que el lote de vuelos no sea un fraude o algo asi 
        
        public async Task<ValidationResult> ProcessFlightBatchAsync(IEnumerable<Flight> batch, string airlineCode, string currentAirport)
        {
            var result = new ValidationResult();

            // AQUI PRegunta, esos vuelos son suyos??
            // Revisamos que American Airlines no esté intentando subir vuelos de Delta por ejm por que seria un error fatal 
            var ownershipResult = _ownershipPolicy.IsAirlineOwnerOfBatch(airlineCode, batch);
            if (!ownershipResult.IsValid)
            {
               // si se mete alguien indebido pues rechazamos todo el lote
                return ownershipResult;
            }

            // estos vuelo operan desde este aeropuert?
            // Revisamos que no nos manden vuelos de Madrid si somos el aeropuerto de SD
            var coherenceResult = _coherencePolicy.IsBatchCoherent(batch, currentAirport);
            if (!coherenceResult.IsValid)
            {
                // Si los lugares no cuadran, rechaza 
                return coherenceResult;
            }

            // si llega aqui, pues paso todo
            return await Task.FromResult(result.Success());
        }

      
        // esto Solo revisa si los vuelos coinciden con el aeropuerto
         //Se usa si la Capa de Aplicación quiere revisar esto por separad
        
        public async Task<ValidationResult> ValidateBatchAsync(IEnumerable<Flight> batch, string airportName)
        {
            // Le pasamos el trabajo sucio a la política de coherencia
            return await Task.FromResult(_coherencePolicy.IsBatchCoherent(batch, airportName));
        }

        // Revisa si es legal cambiarle el estado a un vuelo en este momento
        
        public ValidationResult ValidateInFlight(Flight flight, FlightStateEnum newState)
        {
            var result = new ValidationResult();

            // esto es del sad,  Si el vuelo ya se acabo, aterrizó o se canceloo
            // la historia ya estara escrita, nadie mas podra modificar
            if (flight.FlightStated == FlightStateEnum.Finalizado ||
                flight.FlightStated == FlightStateEnum.AterrizadoArribado ||
                flight.FlightStated == FlightStateEnum.Cancelado)
            {
                // esto devuelveun error que ya está cerrado, que nadie lo toque
                return result.Failur(ErrorAirlines.FlightAlreadyFinalized);
            }

            // Si el vuelo esta vivo, pues permitimos el cambio.
            return result.Success();
        }

        
        //Solo revisa si la aerolínea es la verdadera dueña de estos vuelos.
        
        public async Task<ValidationResult> ValidateOwnerAsync(string airlineCode, IEnumerable<Flight> batch)
        {
            // Le pasamos el trabajo a la política de dueños
            return await Task.FromResult(_ownershipPolicy.IsAirlineOwnerOfBatch(airlineCode, batch));
        }
    }
}