using AeroVeloz.Application.DTOs.Organization.Airports;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Domain.Models.Airports;

namespace AeroVeloz.Application.Contracts.Airport
{
    public interface IAirportConnectionServicie
    {
        Task<OperationResult<bool>> CreateConnectionAsync(ConnectionAirlineByAirportSaveDto dto, Guid userId, int orgId);
        Task<OperationResult<bool>> DeactivateConnectionAsync(Guid connectionId, string airportIcao,  Guid userId, int orgId);
        Task<OperationResult<IReadOnlyCollection<AirlineConnectionByAirportModel>>> GetConnectionsAsync(string codeAirportIcao, Guid userId, int orgId);
    }
}
