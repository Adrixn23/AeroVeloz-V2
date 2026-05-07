using AeroVeloz.Application.Contracts.Airport;
using AeroVeloz.Application.DTOs.Organization.Airports;
using Microsoft.AspNetCore.Mvc;

namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class AirportController : ControllerBase
    {

        public readonly IAirportService _airportService;
        public AirportController(IAirportService airportService   ) { 
            
            _airportService = airportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid userId, [FromQuery] int orgId) {

          var result = await _airportService.GetAllAsync(userId, orgId);
          if(result.Success) return Ok(result);
          return BadRequest(result);
        }


        // GET api/<AirportController>/5

        [HttpGet("{codeAirport}")]
        public async Task<IActionResult>  Get([FromRoute] string codeAirport, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result =  await _airportService.GetByCodeAsync(codeAirport, userId, orgId);
            if(result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        // POST api/<AirportController>
        [HttpPost]
        public async Task<IActionResult>  Post([FromBody] AirportSaveDto airportSaveDto, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _airportService.CreateAsync(airportSaveDto, userId, orgId);
            if(result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("{codeAirport}/generate-api-key")]
        public async Task<IActionResult> Post([FromRoute] string codeAirport, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _airportService.GenerateApiKeyAsync(codeAirport, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }


        // PUT api/<AirportController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AirportUpdateDto airportUpdate, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _airportService.UpdateAsync(airportUpdate, userId, orgId);
            if(result.Success) return Ok( result);
            return BadRequest(result);
        }

        // DELETE api/<AirportController>/5
        [HttpDelete("{entityId}")]
        public async Task<IActionResult> Desactive([FromRoute] int entityId, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _airportService.DeactivateAsync(entityId, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);

        }

         





    }
}
