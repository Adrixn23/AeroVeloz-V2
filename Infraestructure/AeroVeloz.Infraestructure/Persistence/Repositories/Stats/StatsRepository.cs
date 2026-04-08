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
                // Contar aeropuertos: Si orgId es un aeropuerto, devuelve 1, sino 0
                var totalAirports = await _context.Airports
                    .Where(a => a.Id == orgId)
                    .CountAsync();

                var usersData = await (
                    from u in _context.Users.AsNoTracking()
                    join r in _context.Roles.AsNoTracking() on u.idRol equals r.Id
                    where u.idOrganization == orgId
                    select r.nameRol
                ).ToListAsync();

                int totalAdmins = usersData.Count(role => role != null && (role.Contains("Admin", StringComparison.OrdinalIgnoreCase) || role.Contains("SuperAdmin", StringComparison.OrdinalIgnoreCase)));
                int totalOperators = usersData.Count(role => role != null && (role.Contains("Operador", StringComparison.OrdinalIgnoreCase) || role.Contains("Operator", StringComparison.OrdinalIgnoreCase)));

                // Contar vuelos activos asociados a esta organización
                int totalActiveFlights = await (
                    from op in _context.OperationChanges.AsNoTracking()
                    join air in _context.Airports.AsNoTracking() on op.codeAirportIcao equals air.codeAirportIcao
                    where air.Id == orgId
                    select op.Id
                ).CountAsync();

                return new GlobalStatsDto
                {
                    TotalAirports = totalAirports,
                    TotalAdmins = totalAdmins,
                    TotalOperators = totalOperators,
                    TotalActiveFlights = totalActiveFlights
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las estadísticas globales para la organización {OrgId}", orgId);
                return new GlobalStatsDto
                {
                    TotalAirports = 0,
                    TotalAdmins = 0,
                    TotalOperators = 0,
                    TotalActiveFlights = 0
                };
            }
        }
    }
}