using AeroVeloz.Application.Contracts.Flights;
using AeroVeloz.Application.DTOs.Flights;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AeroVeloz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightServicie _flightService;

        public FlightsController(IFlightServicie flightService)
        {
            _flightService = flightService;
        }

        // oobtener vuelos publics activos tipo un dashboard
        [HttpGet("GetPublicActiveFlightsAsync")]
        public async Task<IActionResult> GetPublic()
        {
            var result = await _flightService.GetPublicActiveFlightsAsync();
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        //Obtener vuelos de una aerolineaa
        [HttpGet("GetFlightsByAirlineAsync/{codeAirlines}")]
        public async Task<IActionResult> GetByAirline(string codeAirlines, [FromQuery] int orgId)
        {
            var result = await _flightService.GetFlightsByAirlineAsync(codeAirlines, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        // Obtener vuelos de un aeropuerto 
        [HttpGet("GetPublicFlightsByAirportAsync/{airportCode}")]
        public async Task<IActionResult> GetByAirport(string airportCode)
        {
            var result = await _flightService.GetPublicFlightsByAirportAsync(airportCode);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        // obtener Detalles de un vuelo específico
        [HttpGet("GetFlightDetailAsync/{flightNumber}/{codeAirlines}")]
        public async Task<IActionResult> GetDetail(short flightNumber, string codeAirlines)
        {
            var result = await _flightService.GetFlightDetailAsync(flightNumber, codeAirlines);
            if (result.Success) return Ok(result);
            return NotFound(result); // lanzamos un 444 si no existe
        }

        // Actualizar estado del vuelo usando el fromquery y el from body
        [HttpPut("UpdateStateAsync")]
        public async Task<IActionResult> UpdateState([FromBody] FlightUpdateStateDto dto, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _flightService.UpdateStateAsync(dto, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        // Carga masiva por lotess
        [HttpPost("UploadBatchAsync")]
        public async Task<IActionResult> UploadBatch([FromBody] IEnumerable<FlightBatchItemDto> batch, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _flightService.UploadBatchAsync(batch, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}