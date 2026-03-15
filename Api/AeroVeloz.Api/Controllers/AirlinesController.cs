using AeroVeloz.Application.Contracts.Airlines;
using AeroVeloz.Application.DTOs.Airlines;
using AeroVeloz.Domain.Models.Airline;
using Microsoft.AspNetCore.Mvc;

namespace AeroVeloz.Api.Controllers.Airlines
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirlinesController : ControllerBase
    {
        private readonly IAirlineService _airlineService;

        public AirlinesController(IAirlineService airlineService)
        {
            _airlineService = airlineService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AirlineSaveDto dto, [FromHeader] Guid userId, [FromHeader] int orgId)
        {
            var result = await _airlineService.CreateAirlineAsync(dto, userId, orgId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{codeIcao}")]
        public async Task<IActionResult> Update(string codeIcao, [FromBody] AirlineSaveDto dto, [FromHeader] Guid userId, [FromHeader] int orgId)
        {
            var result = await _airlineService.UpdateAirlineAsync(dto, codeIcao, userId, orgId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{codeIcao}")]
        public async Task<IActionResult> Delete(string codeIcao, [FromHeader] Guid userId, [FromHeader] int orgId)
        {
            var result = await _airlineService.DeleteAirlineAsync(codeIcao, userId, orgId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{codeIcao}")]
        public async Task<IActionResult> GetByCode(string codeIcao)
        {
            var result = await _airlineService.GetAirlineByCodeAsync(codeIcao);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _airlineService.GetAllActiveAirlinesAsync();
            return Ok(result);
        }
    }
}
