using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Entities.Operations;
using AeroVeloz.Domain.Models.Operational;

namespace AeroVeloz.Application.Repositories.Operational
{
    /// <summary>
    /// Interfaz de repositorio para la gestión de cambios operacionales.
    /// Extiende <see cref="IBRepository{TEntity, TId}"/> con operaciones CRUD básicas
    /// y agrega consultas específicas para obtener operaciones por vuelo, por aeropuerto
    /// y por identificador único de operación.
    /// </summary>
    public  interface IOperationalRepository : IBRepository<Domain.Entities.Operations.OperationChange, Guid>
    {

        /// <summary>
        /// Obtiene una operación específica por su identificador único.
        /// </summary>
        /// <param name="operationId">Identificador de la operación a consultar.</param>
        /// <returns>Modelo de la operación encontrada; null si no existe.</returns>
        Task<OperationalModel> GetByOperationAsync(Guid operationId);

        /// <summary>
        /// Obtiene todos los cambios operacionales realizados sobre un vuelo específico.
        /// </summary>
        /// <param name="flightNumber">Número del vuelo a consultar.</param>
        /// <returns>Colección de operaciones realizadas sobre el vuelo.</returns>
        Task<IReadOnlyCollection<OperationalModel>> GetFlightChangesAsync(short flightNumber);

        /// <summary>
        /// Obtiene todos los cambios operacionales realizados en un aeropuerto/organización.
        /// </summary>
        /// <param name="orgId">Identificador de la organización/aeropuerto a consultar.</param>
        /// <returns>Colección de operaciones con detalle del aeropuerto.</returns>
        Task<IReadOnlyCollection<OperationalDetailModel>> GetAirportChangesAsync(int orgId);

    }
}
