
namespace AeroVeloz.Desktop.Services.Interfaces.Airport;

public interface IAirportConnectionService
{
    Task<IEnumerable<Models.DTOs.Connection.ConnectionDto>> GetAirportConnectionsAsync();
    Task<dynamic?> GetConnectionByIdAsync(Guid connectionId);
    Task<bool> CreateConnectionAsync(dynamic connection);
    Task<bool> UpdateConnectionAsync(Guid connectionId, dynamic connection);
    Task<bool> DeleteConnectionAsync(Guid connectionId);
}

