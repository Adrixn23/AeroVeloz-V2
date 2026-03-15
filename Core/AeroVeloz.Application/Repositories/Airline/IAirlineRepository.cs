using AeroVeloz.Application.DTOs.Flights.Base;
using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Models.Airline;

namespace AeroVeloz.Application.Repositories.Airlines
{
    public interface IAirlineRepository : IBRepository<Airline>
    {
        //  Obtener la entidad pura 
        Task<Airline?> GetEntityByCodeAsync(string codeAirlinesIcao);

        // Obtener detalle de aerolínea 
        Task<AirlineDetailModel?> GetDetailByCodeAsync(string codeAirlinesIcao);

        // Verificar si existe una aerolínea por código
        Task<bool> ExistsByCodeAsync(string codeAirlinesIcao);

        //  Obtener todas las aerolíneas activas 
        Task<IReadOnlyCollection<AirlineDetailModel>> GetAllActiveDetailsAsync();

        // Obtener la lista de entidades de aerolíneas activas
        Task<IReadOnlyCollection<Airline>> GetAllActiveEntitiesAsync();
    }
}