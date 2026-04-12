using AeroVeloz.Application.Contracts.Flights;
using AeroVeloz.Application.DTOs.Flights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroVeloz.Api.Controllers
{
    [Route("api/flights")]
    [Authorize]
    public class FlightsController : ApiBaseController
    {
        private readonly IFlightService _flightService;

        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<ActionResult<IReadOnlyCollection<FlightReadDto>>> GetPublic()
        {
            var result = await _flightService.GetPublicActiveFlightsAsync();
            return ProcessResult(result);
        }

        [HttpGet("airline/{codeAirlines}")]
        public async Task<ActionResult<IReadOnlyCollection<FlightReadDto>>> GetByAirline(string codeAirlines, [FromQuery] int orgId)
        {
            var result = await _flightService.GetFlightsByAirlineAsync(codeAirlines, orgId);
            return ProcessResult(result);
        }

        [HttpGet("airport/{airportCode}")]
        [AllowAnonymous]
        public async Task<ActionResult<IReadOnlyCollection<FlightReadDto>>> GetByAirport(string airportCode)
        {
            var result = await _flightService.GetPublicFlightsByAirportAsync(airportCode);
            return ProcessResult(result);
        }

        [HttpGet("{flightNumber}/{codeAirlines}")]
        [AllowAnonymous]
        public async Task<ActionResult<FlightReadDto>> GetDetail(short flightNumber, string codeAirlines)
        {
            var result = await _flightService.GetFlightDetailAsync(flightNumber, codeAirlines);
            return ProcessResult(result);
        }

        [HttpPut("state")]
        public async Task<ActionResult> UpdateState([FromBody] FlightUpdateStateDto dto, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _flightService.UpdateStateAsync(dto, userId, orgId);
            return ProcessNoContentResult(result);
        }

        [HttpPost("batch")]
        public async Task<ActionResult<FlightBatchResultDto>> UploadBatch([FromBody] IEnumerable<FlightBatchItemDto> batch, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _flightService.UploadBatchAsync(batch, userId, orgId);
            return ProcessResult(result);
        }
    }
}
