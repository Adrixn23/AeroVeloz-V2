using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Entities.Organization.Airport;
using AeroVeloz.Domain.Models.Airports;

namespace AeroVeloz.Application.Repositories.Airport
{
    /// <summary>
    /// Interfaz de repositorio para la gestión de conexiones entre aeropuertos y aerolíneas.
    /// Extiende <see cref="IBRepository{TEntity, TId}"/> con operaciones CRUD básicas
    /// y agrega consultas específicas para obtener conexiones por aeropuerto.
    /// </summary>
    public interface IAirportConnectionAirline : IBRepository<ConectionsAirlineAirport, Guid>
    {
        /// <summary>
        /// Obtiene todas las conexiones de aerolíneas asociadas a un aeropuerto específico.
        /// </summary>
        /// <param name="codeAirportIcao">Código ICAO del aeropuerto a consultar.</param>
        /// <returns>Colección de conexiones del aeropuerto con información de las aerolíneas.</returns>
        Task<IReadOnlyCollection<AirlineConnectionByAirportModel>> GetAirportConnectionById(string? codeAirportIcao);

    }
}
