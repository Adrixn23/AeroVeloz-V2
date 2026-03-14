using AeroVeloz.Application.Contracts.Subscriptions;
using AeroVeloz.Application.DTOs.Subscriptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AeroVeloz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        
        private readonly ISubscriptionServicie _subscriptionService;

        public SubscriptionController(ISubscriptionServicie subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        // Crear una suscripcion a un vuelo
        [HttpPost("SubscribeExternalAsync")]
        public async Task<IActionResult> SubscribeExternal([FromBody] SubscriptionSaveDto dto)
        {
            var result = await _subscriptionService.SubscribeExternalAsync(dto);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        // Cancelar una suscripcion existente
        [HttpDelete("CancelSubscriptionAsync/{id}")]
        public async Task<IActionResult> CancelSubscription(Guid id)
        {
            var result = await _subscriptionService.CancelSubscriptionAsync(id);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        // Obtener suscripciones por vuelo
        [HttpGet("GetByFlightAsync/{flightNumber}/{codeAirlines}")]
        public async Task<IActionResult> GetByFlight(short flightNumber, string codeAirlines)
        {
            var result = await _subscriptionService.GetByFlightAsync(flightNumber, codeAirlines);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        // Obtener cantidad de interesados por vuelo
        [HttpGet("GetInterestedCountAsync/{flightNumber}/{codeAirlines}")]
        public async Task<IActionResult> GetInterestedCount(short flightNumber, string codeAirlines)
        {
            var result = await _subscriptionService.GetInterestedCountAsync(flightNumber, codeAirlines);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }
    }
}
