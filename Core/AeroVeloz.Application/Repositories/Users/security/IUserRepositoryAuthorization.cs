namespace AeroVeloz.Application.Repositories.Users.security
{
    public interface IUserRepositoryAuthorization { //esta interface contiene los elementos que conllevan a la consulta
        //y return de elementos de authrozation segun el usuario y organismo al cual pertenezca el mismo.

        Task<bool> AuthorizeFlightAccessAsync(Guid userId, int flightNumber, string airlineCode);
        Task<bool> AuthorizeAirportAccessAsync(Guid userId, string airportCode);
        Task<IEnumerable<string>> GetUserRolesAsync(Guid userId);
        Task<bool> IsSuperAdminAsync(Guid userId);
        Task<bool> IsAirportAdminAsync(Guid userId, string airportCode);
        Task<bool> CanModifyFlightAsync(Guid userId, int flightNumber);
        Task<bool> CanViewAuditLogsAsync(Guid userId); 


    }

}
