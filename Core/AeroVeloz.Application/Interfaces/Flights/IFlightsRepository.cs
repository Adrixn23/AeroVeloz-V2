using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using AeroVeloz.Domain.Entities.Flight;
namespace AeroVeloz.Infraestructure.Persistence.Interfaces.Flights
{
    public interface IFlightsRepository : IBRepository<Domain.Entities.Flight.Flights, short>
    {

        // crear modelo de lectura metodos minimos que la unidad devuelva, numero de vuelo con codigo de aerolinea, cod de aerolinea y estado pa los pasajeros/visitante


        // 1. Sobrecarga del GetById
        //Task<AeroVeloz.Domain.Entities.Flight.Flights?> GetByIdAsync(short flightNumber, string airlineCode);

        //// 2. Consulta para el dashboard
        //Task<IReadOnlyCollection<FlightEntity>> GetOperationalFlightsAsync();

        //// 3. Buqueda con filtros
        //Task<IReadOnlyCollection<FlightEntity>> SearchFlightsAsync(
        //     string? airlineCode,
        //     string? origin,
        //     string? destination,
        //    byte? statusId);

        //// 4. Verificar existencia
        //Task<bool> ExistsAsync(short flightNumber, string airlineCode);
    }
}





