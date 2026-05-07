using AeroVeloz.Application.DTOs.Airlines;
using AeroVeloz.Application.Services.Result;
using AeroVeloz.Domain.Models.Airline;

namespace AeroVeloz.Application.Contracts.Airlines
{
    public interface IAirlineService
    {
        Task<OperationResult<bool>> CreateAirlineAsync(AirlineSaveDto dto, Guid userId, int orgId);
        Task<OperationResult<bool>> UpdateAirlineAsync(AirlineSaveDto dto, string codeAirlinesIcao, Guid userId, int orgId);
        Task<OperationResult<bool>> DeleteAirlineAsync(string codeAirlinesIcao, Guid userId, int orgId);
        Task<OperationResult<AirlineDetailModel>> GetAirlineByCodeAsync(string codeAirlinesIcao);
        Task<OperationResult<IReadOnlyCollection<AirlineDetailModel>>> GetAllActiveAirlinesAsync();
    }
}