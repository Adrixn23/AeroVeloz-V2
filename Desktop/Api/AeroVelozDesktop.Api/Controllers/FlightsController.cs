using AeroVeloz.Application.Contracts.Flights;
using AeroVeloz.Application.Repositories.Operational;
using AeroVelozDesktop.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IOperationalRepository _operationalRepository;

        public FlightsController(IFlightService flightService, IOperationalRepository operationalRepository)
        {
            _flightService = flightService;
            _operationalRepository = operationalRepository;
        }

        /// <summary>
        /// Obtiene todos los vuelos activos con detalles de operaciones
        /// </summary>
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveFlights()
        {
            var userId = this.GetUserId();
            var orgId = this.GetOrganizationId();
            var result = await _flightService.GetAllActiveFlightsAsync(userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Obtiene vuelos activos para un aeropuerto específico con detalles de operaciones
        /// </summary>
        [HttpGet("airport/{airportCode}")]
        public async Task<IActionResult> GetFlightsByAirport([FromRoute] string airportCode)
        {
            var userId = this.GetUserId();
            var orgId = this.GetOrganizationId();
            var result = await _flightService.GetFlightsByAirportAsync(airportCode, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Obtiene detalles completos de un vuelo específico
        /// </summary>
        [HttpGet("{flightId}")]
        public async Task<IActionResult> GetFlightDetails([FromRoute] short flightId)
        {
            var userId = this.GetUserId();
            var orgId = this.GetOrganizationId();
            var result = await _flightService.GetFlightDetailsAsync(flightId, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Obtiene todas las operaciones activas de un vuelo específico
        /// </summary>
        [HttpGet("{flightId}/operations")]
        public async Task<IActionResult> GetFlightOperations([FromRoute] short flightId)
        {
            try
            {
                var operations = await _operationalRepository.GetFlightOperationsAsync(flightId);
                if (operations != null && operations.Count > 0)
                {
                    return Ok(new { success = true, value = operations.Where(op => op.IsActive), message = "Operaciones obtenidas" });
                }
                return Ok(new { success = true, value = new List<object>(), message = "No hay operaciones para este vuelo" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Error obteniendo operaciones: {ex.Message}" });
            }
        }
    }
}
