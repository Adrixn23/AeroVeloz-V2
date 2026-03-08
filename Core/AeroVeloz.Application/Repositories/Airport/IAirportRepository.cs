using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Models.Airports;
namespace AeroVeloz.Application.Repositories.Airport
{
    /// <summary>
    /// Interfaz de repositorio para la gestión de aeropuertos.
    /// Extiende <see cref="IBRepository{TEntity, TId}"/> con operaciones CRUD básicas
    /// y agrega consultas específicas para obtener aeropuertos y generar API keys.
    /// </summary>
    public  interface IAirportRepository : IBRepository<Domain.Entities.Organization.Airports.Airport, string>
    {
        /// <summary>
        /// Obtiene todos los aeropuertos registrados en el sistema.
        /// Requiere que el usuario tenga rol de SYSTEM_ADMIN.
        /// </summary>
        /// <returns>Colección de aeropuertos con su información básica.</returns>
        Task<IReadOnlyCollection<AirportModel>> GetAllAirport();

        /// <summary>
        /// Obtiene la información de un aeropuerto específico por su código ICAO.
        /// </summary>
        /// <param name="codeAirport">Código ICAO del aeropuerto a consultar.</param>
        /// <returns>Modelo del aeropuerto encontrado; null si no existe.</returns>
        Task<AirportModel> GetAirportByCode(string? codeAirport);

        /// <summary>
        /// Genera una nueva clave API maestra de forma segura para un aeropuerto.
        /// Utiliza generación criptográfica de bytes aleatorios.
        /// </summary>
        /// <param name="codeAirport">Código ICAO del aeropuerto al que generar la API key.</param>
        /// <returns>True si la clave fue generada y almacenada exitosamente.</returns>
        Task<bool> GenerateApiKey(string? codeAirport);
    }

}
