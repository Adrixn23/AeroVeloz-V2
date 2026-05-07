using AeroVeloz.Application.Contracts.Airlines;
using AeroVeloz.Application.DTOs.Airlines;
using AeroVeloz.Application.Services.Result;
using AeroVeloz.Domain.Models.Airline;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroVeloz.Api.Controllers.Airlines
{
    [Route("api/airlines")]
    [Authorize]
    public class AirlineController : ApiBaseController
    {
        private readonly IAirlineService _airlineService;

        public AirlineController(IAirlineService airlineService)
        {
            _airlineService = airlineService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(OperationResult<IReadOnlyCollection<AirlineDetailModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<AirlineDetailModel>>> GetAll()
        {
            var result = await _airlineService.GetAllActiveAirlinesAsync();
            return ProcessResult(result);
        }

        [HttpGet("{codeIcao}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(OperationResult<AirlineDetailModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AirlineDetailModel>> GetByCode(string codeIcao)
        {
            var result = await _airlineService.GetAirlineByCodeAsync(codeIcao);
            return ProcessResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<bool>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> Create([FromBody] AirlineSaveDto dto, [FromHeader] Guid userId, [FromHeader] int orgId)
        {
            var result = await _airlineService.CreateAirlineAsync(dto, userId, orgId);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetByCode), new { codeIcao = dto.CodeAirlinesIcao }, result);
            }
            return ProcessResult(result);
        }

        [HttpPut("{codeIcao}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(string codeIcao, [FromBody] AirlineSaveDto dto, [FromHeader] Guid userId, [FromHeader] int orgId)
        {
            var result = await _airlineService.UpdateAirlineAsync(dto, codeIcao, userId, orgId);
            return ProcessNoContentResult(result);
        }

        [HttpDelete("{codeIcao}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(string codeIcao, [FromHeader] Guid userId, [FromHeader] int orgId)
        {
            var result = await _airlineService.DeleteAirlineAsync(codeIcao, userId, orgId);
            return ProcessNoContentResult(result);
        }
    }
}
