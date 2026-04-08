using AeroVeloz.Application.Contracts.Airport;
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
        private readonly AeroVeloz.Application.Contracts.StatusSystem.IStatsService _statsService;

        public StatsController(AeroVeloz.Application.Contracts.StatusSystem.IStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet("global")]
        public async Task<IActionResult> GetGlobalStats()
        {
            var userId = this.GetUserId();
            var orgId = this.GetOrganizationId();

            var result = await _statsService.GetGlobalStatsAsync(userId, orgId);
            if (result.Success)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Message);
        }
    }
}
