using AeroVeloz.Application.Contracts.Airport;
using AeroVeloz.Application.Contracts.Operations;
using AeroVeloz.Application.Contracts.Users;
using AeroVeloz.Application.DTOs.StatusSystem;
using AeroVelozDesktop.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatsController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly IUserService _userService;
        private readonly IOperationalService _operationalService;

        public StatsController(
            IAirportService airportService,
            IUserService userService,
            IOperationalService operationalService)
        {
            _airportService = airportService;
            _userService = userService;
            _operationalService = operationalService;
        }

        [HttpGet("global")]
        public async Task<IActionResult> GetGlobalStats()
        {
            var userId = this.GetUserId();
            var orgId = this.GetOrganizationId();

            var airportsResult = await _airportService.GetAllAsync(userId, orgId);
            int totalAirports = airportsResult.Success && airportsResult.Value != null ? airportsResult.Value.Count : 0;

            var usersResult = await _userService.GetUsersByOrganizationAsync(userId, orgId);
            var users = usersResult.Success && usersResult.Value != null ? usersResult.Value : new List<AeroVeloz.Domain.Models.Users.UserDetailModel>();
            
            int totalAdmins = users.Count(u => u.nameRol != null && (u.nameRol.Contains("Admin", StringComparison.OrdinalIgnoreCase) || u.nameRol.Contains("SuperAdmin", StringComparison.OrdinalIgnoreCase)));
            int totalOperators = users.Count(u => u.nameRol != null && (u.nameRol.Contains("Operador", StringComparison.OrdinalIgnoreCase) || u.nameRol.Contains("Operator", StringComparison.OrdinalIgnoreCase)));

            var flightsResult = await _operationalService.GetAirportChangesAsync(userId, orgId);
            int totalActiveFlights = flightsResult.Success && flightsResult.Value != null ? flightsResult.Value.Count : 0;

            var dto = new GlobalStatsDto
            {
                TotalAirports = totalAirports,
                TotalAdmins = totalAdmins,
                TotalOperators = totalOperators,
                TotalActiveFlights = totalActiveFlights
            };

            return Ok(dto);
        }
    }
}
