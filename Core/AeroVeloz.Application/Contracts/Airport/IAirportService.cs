using AeroVeloz.Application.Contracts.Base;
using AeroVeloz.Application.DTOs.Organization.Airports;
using AeroVeloz.Application.Handlers.Result;
using AeroVeloz.Domain.Models.Airports;

namespace AeroVeloz.Application.Contracts.Airport
{
    public interface IAirportService : IBaseService<AirportSaveDto, AirportUpdateDto, int>
    {
        Task<OperationResult<IReadOnlyCollection<AirportModel>>> GetAllAsync(Guid userId, int orgId);
        Task<OperationResult<AirportModel>> GetByCodeAsync(string codeAirport, Guid userId, int orgId);
        Task<OperationResult<bool>> GenerateApiKeyAsync(string codeAirport, Guid userId, int orgId);
    }
}
