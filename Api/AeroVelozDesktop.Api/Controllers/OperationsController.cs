using AeroVeloz.Application.Contracts.Operations;
using AeroVeloz.Application.DTOs.Operations;
using AeroVeloz.Domain.Entities.Users.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {

        public readonly IOperationalServicie _operationalServicie;

        public OperationsController(IOperationalServicie operationalServicie) { 
            _operationalServicie = operationalServicie;
        }


        // GET: api/<OperationsController>
        [HttpGet("GetFlightChangesAsync/{flightNumber}")]
        public async Task<IActionResult> GetByFlights(short flightNumber, Guid userId, int orgId)
        {
           var result = await _operationalServicie.GetFlightChangesAsync(flightNumber, userId, orgId);
            if(result.Success) Ok(result);
            return BadRequest(result);
        }

        // GET: api/<OperationsController>
        [HttpGet("GetAirportChangesAsync/{orgId}")]
        public async Task<IActionResult> GetByAirport(Guid userId, int orgId)
        {
            var result = await _operationalServicie.GetAirportChangesAsync(userId, orgId);
            if (result.Success) Ok(result);
            return BadRequest(result);
        }



        // GET api/<OperationsController>/5
        [HttpGet("GetById/{operationId}")]
        public async Task<IActionResult> GetById(Guid operationId, Guid userId, int orgId)
        {
            var result = await _operationalServicie.GetByIdAsync(operationId, userId, orgId);
            if (result.Success) Ok(result);
            return BadRequest(result);
        }

        // POST api/<OperationsController
        [HttpPost("{RegisterAsync}")]
        public async Task<IActionResult> Post(OperationalChangeSaveDto dto, Guid userId, int orgId)
        {
            var result = await  _operationalServicie.RegisterAsync(dto, userId, orgId);
            if (result.Success) Ok(result);
            return BadRequest(result);
        }

        ///agregar llamado de desactive para este service 
       
    }
}
