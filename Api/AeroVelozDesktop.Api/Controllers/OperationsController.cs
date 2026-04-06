using AeroVeloz.Application.Contracts.Operations;
using AeroVeloz.Application.DTOs.Operations;
using Microsoft.AspNetCore.Mvc;

namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {

        public readonly IOperationalService _operationalService;

        public OperationsController(IOperationalService operationalService) { 
            _operationalService = operationalService;
        }


        // GET: api/<OperationsController>
        [HttpGet("GetFlightChangesAsync/{flightNumber}")]
        public async Task<IActionResult> GetByFlights(short flightNumber, Guid userId, int orgId)
        {
           var result = await _operationalService.GetFlightChangesAsync(flightNumber, userId, orgId);
            if(result.Success) Ok(result);
            return BadRequest(result);
        }

        // GET: api/<OperationsController>
        [HttpGet("GetAirportChangesAsync/{orgId}")]
        public async Task<IActionResult> GetByAirport(Guid userId, int orgId)
        {
            var result = await _operationalService.GetAirportChangesAsync(userId, orgId);
            if (result.Success) Ok(result);
            return BadRequest(result);
        }

        // GET api/<OperationsController>/5
        [HttpGet("GetById/{operationId}")]
        public async Task<IActionResult> GetById(Guid operationId, Guid userId, int orgId)
        {
            var result = await _operationalService.GetByIdAsync(operationId, userId, orgId);
            if (result.Success) Ok(result);
            return BadRequest(result);
        }

        // POST api/<OperationsController
        [HttpPost("{RegisterAsync}")]
        public async Task<IActionResult> Post(OperationalChangeSaveDto dto, Guid userId, int orgId)
        {
            var result = await  _operationalService.RegisterAsync(dto, userId, orgId);
            if (result.Success) Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{Desactive}")]
        public async Task<IActionResult> Desactive(OperationalChangeRemoveDto dto, Guid userId, int orgId)
        {
            var result = await _operationalService.DesactiveOperational(dto, userId, orgId);
            if (result.Success) Ok(result);
            return BadRequest(result);
        }
     
    }
}
