using AeroVeloz.Application.Contracts.Airport;
using AeroVeloz.Application.DTOs.Organization.Airports;
using Microsoft.AspNetCore.Mvc;

namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {

        public readonly IAirportServicie _airportServicie;
        public AirportController(IAirportServicie airportServicie   ) { 
            
            _airportServicie = airportServicie;
        }

        [HttpGet("{GetAllAsync}")]

        public async Task<IActionResult> GetAll(Guid userId, int orgId) {
                
          var result = await _airportServicie.GetAllAsync(userId, orgId);
          if(result.Success) return Ok(result);
          return BadRequest(result);
        }


        // GET api/<AirportController>/5

        [HttpGet("GetByCodeAsync/{codeAirport}")]
        public async Task<IActionResult>  Get(string codeAirport, Guid userId, int orgId)
        {
            var result =  await _airportServicie.GetByCodeAsync(codeAirport, userId, orgId);
            if(result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        // POST api/<AirportController>
        [HttpPost("{CreateAsync}")]
        public async Task<IActionResult>  Post(AirportSaveDto airportSaveDto, Guid userId, int orgId)
        {
            var result = await _airportServicie.CreateAsync(airportSaveDto, userId, orgId);
            if(result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("GenerateApiKeyAsync/{codeAirport}")]

        public async Task<IActionResult> Post(string codeAirport, Guid userId, int orgId)
        {
            var result = await _airportServicie.GenerateApiKeyAsync(codeAirport, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }



        // PUT api/<AirportController>/5
        [HttpPut("{UpdateAsync}")]
        public async Task<IActionResult> Put(AirportUpdateDto airportUpdate, Guid userId, int orgId)
        {
            var result = await _airportServicie.UpdateAsync(airportUpdate, userId, orgId);
            if(result.Success) return Ok( result);
            return BadRequest(result);
        }

        // DELETE api/<AirportController>/5
        [HttpDelete("{DeactivateAsync}")]
        public async Task<IActionResult> Desactive(int entityId, Guid userId, int orgId)
        {
            var result = await _airportServicie.DeactivateAsync(entityId, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);

        }

         





    }
}
