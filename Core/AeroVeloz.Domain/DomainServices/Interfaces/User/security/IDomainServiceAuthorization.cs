namespace AeroVeloz.Domain.DomainServices.Interfaces.User.security
{
    public interface IDomainServiceAuthorization {
        Task<bool> AuthorizeOperationalActionAsync(Guid userId, string action, string resource); 
        Task<bool> AuthorizeFlightAccessAsync(Guid userId, int flightNumber, string airlineCode); 
        Task<bool> AuthorizeAirportAccessAsync(Guid userId, string airportCode); 
        Task<IEnumerable<string>> GetUserRolesAsync(Guid userId); 
        Task<bool> IsSuperAdminAsync(Guid userId); Task<bool> IsAirportAdminAsync(Guid userId, string airportCode);
        Task<bool> CanModifyFlightAsync(Guid userId, int flightNumber);
        Task<bool> CanViewAuditLogsAsync(Guid userId); }
}
