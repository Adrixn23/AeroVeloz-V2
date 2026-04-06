using AeroVeloz.Application.Contracts.Airport;
using AeroVeloz.Application.DTOs.Organization.Airports;
using AeroVeloz.Domain.Entities.Users.User;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportConnectionController : ControllerBase
    {


        public readonly IAirportConnectionServicie _airportConnectionServicie;
        public AirportConnectionController(IAirportConnectionServicie airportConnectionServicie) { 
            _airportConnectionServicie = airportConnectionServicie;
        }



        // GET: api/<AirportConnectionController>
        [HttpGet]
        public async Task<IActionResult>  GetAll(string codeAirportIcao, Guid userId, int orgId)
        {
            var result = await _airportConnectionServicie.GetConnectionsAsync(codeAirportIcao, userId, orgId);
            if(result.Success) return Ok(result);
            return BadRequest(result);
        }

     

        // POST api/<AirportConnectionController>
        [HttpPost("{CreateConnectionAsync}")]
        public async Task<IActionResult>  Post(ConnectionAirlineByAirportSaveDto dto, Guid userId, int orgId)
        {
            var result = await _airportConnectionServicie.CreateConnectionAsync(dto, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }


     
        // DELETE api/<AirportConnectionController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult>  Desactive(Guid connectionId, string airportIcao, Guid userId, int orgId)
        {
            var result = await _airportConnectionServicie.DeactivateConnectionAsync(connectionId, airportIcao,  userId,  orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
