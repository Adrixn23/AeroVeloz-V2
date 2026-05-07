using AeroVeloz.Application.DTOs.StatusSystem;
using AeroVeloz.Application.Repositories.StatusSystem;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.StatusSystem
{
    public class StatsRepository : IStatsRepository
    {
        private readonly AeroVelozContext _context;
        private readonly ILogger<StatsRepository> _logger;

        public StatsRepository(AeroVelozContext context, ILogger<StatsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GlobalStatsDto> GetGlobalStatsAsync(int orgId)
        {
            try
            {
                // Global stats should return all airports if this is for super admin
                // Initially it was filtering by orgId: .Where(a => a.Id == orgId) 
                // which resulted in 0 or 1 airport.
                var totalAirports = await _context.Airports.CountAsync();

                var usersData = await (
                    from u in _context.Users.AsNoTracking()
                    join r in _context.Roles.AsNoTracking() on u.idRol equals r.Id
                    select r.nameRol
                ).ToListAsync();

                int totalAdmins = usersData.Count(role => role != null && (role.Contains("Admin", StringComparison.OrdinalIgnoreCase) || role.Contains("SuperAdmin", StringComparison.OrdinalIgnoreCase)));
                int totalAirlines = await _context.Airlines.CountAsync();

                return new GlobalStatsDto
                {
                    TotalAirports = totalAirports,
                    TotalAdmins = totalAdmins,
                    TotalAirlines = totalAirlines
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las estadísticas globales para la organización {OrgId}", orgId);
                return new GlobalStatsDto
                {
                    TotalAirports = 0,
                    TotalAdmins = 0,
                    TotalAirlines = 0
                };
            }
        }

        public async Task<AirportAdminStatsDto> GetAirportStatsAsync(int orgId)
        {
            try
            {
                // Get airport stats by organization ID
                var airport = await _context.Airports.FirstOrDefaultAsync(a => a.Id == orgId);

                if (airport == null)
                {
                    return new AirportAdminStatsDto
                    {
                        ContactedAirlines = 0,
                        TotalOperators = 0,
                        ActiveConnections = 0,
                        PendingOperations = 0
                    };
                }

                // Count contacted airlines (via connections)
                var contactedAirlines = await _context.ConectionsAirlineAirports
                    .Where(c => c.codeAirportIcao == airport.codeAirportIcao && c.isActive)
                    .Select(c => c.codeAirlinesIcao)
                    .Distinct()
                    .CountAsync();

                // Count total operators for this airport
                var totalOperators = await _context.Users
                    .Where(u => u.idOrganization == orgId)
                    .CountAsync();

                // Count active connections for this airport
                var activeConnections = await _context.ConectionsAirlineAirports
                    .Where(c => c.codeAirportIcao == airport.codeAirportIcao && c.isActive)
                    .CountAsync();

                // Count pending operations for this airport
                var pendingOperations = await _context.OperationChanges
                    .Where(o => o.codeAirportIcao == airport.codeAirportIcao && o.isActive)
                    .CountAsync();

                return new AirportAdminStatsDto
                {
                    ContactedAirlines = contactedAirlines,
                    TotalOperators = totalOperators,
                    ActiveConnections = activeConnections,
                    PendingOperations = pendingOperations
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las estadísticas del aeropuerto para la organización {OrgId}", orgId);
                return new AirportAdminStatsDto
                {
                    ContactedAirlines = 0,
                    TotalOperators = 0,
                    ActiveConnections = 0,
                    PendingOperations = 0
                };
            }
        }
    }
}
