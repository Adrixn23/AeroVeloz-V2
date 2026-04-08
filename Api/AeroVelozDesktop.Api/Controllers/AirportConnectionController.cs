using AeroVeloz.Application.Contracts.Airport;
using AeroVeloz.Application.DTOs.Organization.Airports;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class AirportConnectionController : ControllerBase
    {


        public readonly IAirportConnectionService _airportConnectionService;
        public AirportConnectionController(IAirportConnectionService airportConnectionService) { 
            _airportConnectionService = airportConnectionService;
        }



        // GET: api/<AirportConnectionController>
        [HttpGet]
        public async Task<IActionResult>  GetAll([FromQuery] string codeAirportIcao, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _airportConnectionService.GetConnectionsAsync(codeAirportIcao, userId, orgId);
            if(result.Success) return Ok(result);
            return BadRequest(result);
        }



        // POST api/<AirportConnectionController>
        [HttpPost]
        public async Task<IActionResult>  Post([FromBody] ConnectionAirlineByAirportSaveDto dto, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _airportConnectionService.CreateConnectionAsync(dto, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }



        // DELETE api/<AirportConnectionController>/5
        [HttpDelete("{connectionId}")]
        public async Task<IActionResult>  Desactive([FromRoute] Guid connectionId, [FromQuery] string airportIcao, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _airportConnectionService.DeactivateConnectionAsync(connectionId, airportIcao,  userId,  orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
