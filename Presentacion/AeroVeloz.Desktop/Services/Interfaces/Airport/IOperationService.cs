namespace AeroVeloz.Desktop.Services.Interfaces.Airport;

public interface IOperationService
{
    Task<IEnumerable<Models.DTOs.Operation.OperationDto>> GetAirportOperationsAsync();
    Task<dynamic?> GetOperationByIdAsync(Guid operationId);
    Task<bool> CreateOperationAsync(dynamic operation);
    Task<bool> UpdateOperationAsync(Guid operationId, dynamic operation);
    Task<bool> DeleteOperationAsync(Guid operationId);
}

