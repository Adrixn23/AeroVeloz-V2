using AeroVeloz.Application.Models.flights;
using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Entities.Airlines;
using AeroVeloz.Domain.Entities.Airports;
using AeroVeloz.Domain.Entities.Flight;
using AeroVeloz.Domain.Entities.Users;

namespace AeroVeloz.Application.Repositories.UseAdmin
{
    public interface IUserRepository :  IBRepository<User, Guid>
    {
        Task<IEnumerable<Airport>> GetAllAirportsAsync();
        Task<IEnumerable<Airline>> GetAllAirlinesAsync();
        Task<IEnumerable<User>> GetUsersSystemAsync();
        Task<IEnumerable<User>> GetAllSystemOrganizationsAsync();
        
    }
}
