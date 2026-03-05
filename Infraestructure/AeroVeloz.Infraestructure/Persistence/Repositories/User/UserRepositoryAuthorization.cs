using AeroVeloz.Application.Repositories.Users.security;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.User
{
    public class UserRepositoryAuthorization : IUserRepositoryAuthorization
    {
        public Task<bool> AuthorizeAirportAccessAsync(Guid userId, string airportCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AuthorizeFlightAccessAsync(Guid userId, int flightNumber, string airlineCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CanModifyFlightAsync(Guid userId, int flightNumber)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CanViewAuditLogsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetUserRolesAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAirportAdminAsync(Guid userId, string airportCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsSuperAdminAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
