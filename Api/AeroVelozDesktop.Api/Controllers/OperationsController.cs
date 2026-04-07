using AeroVeloz.Application.Contracts.Operations;
using AeroVeloz.Application.DTOs.Operations;
using AeroVelozDesktop.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class OperationsController : ControllerBase
    {

        public readonly IOperationalService _operationalService;

        public OperationsController(IOperationalService operationalService) { 
            _operationalService = operationalService;
        }


        // GET: api/<OperationsController>
        [HttpGet("flights/{flightNumber}/changes")]
        public async Task<IActionResult> GetByFlights([FromRoute] short flightNumber)
        {
            var userId = this.GetUserId();
            var orgId = this.GetOrganizationId();
            var result = await _operationalService.GetFlightChangesAsync(flightNumber, userId, orgId);
            if(result.Success) return Ok(result);
            return BadRequest(result);
        }

        // GET: api/<OperationsController>
        [HttpGet("airports/changes")]
        public async Task<IActionResult> GetByAirport()
        {
            var userId = this.GetUserId();
            var orgId = this.GetOrganizationId();
            var result = await _operationalService.GetAirportChangesAsync(userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        // GET api/<OperationsController>/5
        [HttpGet("{operationId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid operationId)
        {
            var userId = this.GetUserId();
            var orgId = this.GetOrganizationId();
            var result = await _operationalService.GetByIdAsync(operationId, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        // POST api/<OperationsController
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OperationalChangeSaveDto dto)
        {
            var userId = this.GetUserId();
            var orgId = this.GetOrganizationId();
            var result = await  _operationalService.RegisterAsync(dto, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Desactive([FromBody] OperationalChangeRemoveDto dto)
        {
            var userId = this.GetUserId();
            var orgId = this.GetOrganizationId();
            var result = await _operationalService.DesactiveOperational(dto, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

    }
}
