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
    }
}