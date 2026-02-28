using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.TransitionPolices.interfaces.InterfacesAirline;
using AeroVeloz.Domain.ValidationBase;
using AeroVeloz.Domain.Validators.codeError.codeError_Airlines;


namespace AeroVeloz.Domain.TransitionPolices.Polices.PolicyAirlines
{
    class BatchCoherencePolicy : IBatchCoherencePolicy
    {

        // Valida la coherencia geográfica de un lote de vuelos respecto al aeropuerto receptor.
        public ValidationResult IsBatchCoherent(IEnumerable<Flight> batch, string airportName)
        {
            var result = new ValidationResult();

            if (batch == null || !batch.Any())

                // reutilizamo el error de la incongruencia de lote
                return result.Failur(ErrorAirlines.InvalidBatchAirline);





            bool allMatch = batch.All(f => f.OriginAirport == airportName || f.DestinationAirport == airportName);
            if (!allMatch)
                          {
                              // los aeropuertos del lote de vuelo no coinciden con el aeropuerto receptor
                 return result.Failur(ErrorAirlines.InvalidBatchAirline);
                           }
            // El lote es coherente con la ubicación geográfica del aeropuerto
            return result.Success();
        }
    }
}
