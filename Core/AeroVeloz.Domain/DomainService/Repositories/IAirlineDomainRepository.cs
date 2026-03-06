using System.Collections.Generic;

namespace AeroVeloz.Domain.DomainService.Repositories
{
    public interface IAirlineDomainRepository
    {
        // este verifica si la aerolinea existe o esta activa
        // usado por IairlineDomainService: ProcessFlightBatchAsync
        Task<bool> IsAirlineActiveAsync(string airlineCode);



        // este verifica si esta aerolinea es dueño de este lote?
        // Usado por: IAirlineDomainService: ValidateOwnerAsync


        Task<bool> IsAirlineOwnerOfBatchAsync(string airlineCode, IEnumerable<short> flightNumbers);

       

        // Verifica si los aeropuertos del lote coinciden con el aeropuerto receptor
        // Usado por: IAirlineDomainService → ValidateBatchAsync
        Task<bool> AirportMatchesBatchAsync(string airportCode, IEnumerable<short> flightNumbers);

        // Verifica si un vuelo ya está en el aire (despegado)
        // Usado por: IAirlineDomainService: IsValidStateChangeForActiveFlightAsync
        Task<bool> IsFlightAirborneAsync(short flightNumber);



    }
}
