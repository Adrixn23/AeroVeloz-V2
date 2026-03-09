using AeroVeloz.Application.DTOs.Flights.Base;
using AeroVeloz.Domain.Entities.Airlines;

using AeroVeloz.Domain.Entities.Organization.Airports;
using AeroVeloz.Domain.Entities.Users;
using AeroVeloz.Domain.Entities.Users.User;

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
