using AeroVeloz.Application.Contracts.Subscriptions;
using AeroVeloz.Application.DTOs.Subscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroVeloz.Api.Controllers
{
    [Route("api/subscriptions")]
    [Authorize]
    public class SubscriptionController : ApiBaseController
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost("external")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> SubscribeExternal([FromBody] SubscriptionSaveDto dto)
        {
            var result = await _subscriptionService.SubscribeExternalAsync(dto);
            if (result.Success)
            {
                return Ok(result);
            }

            return ProcessResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> CancelSubscription(Guid id)
        {
            var result = await _subscriptionService.CancelSubscriptionAsync(id);
            return ProcessNoContentResult(result);
        }

        [HttpGet("flight/{flightNumber}/{codeAirlines}")]
        public async Task<ActionResult<IReadOnlyCollection<SubscriptionReadDto>>> GetByFlight(short flightNumber, string codeAirlines)
        {
            var result = await _subscriptionService.GetByFlightAsync(flightNumber, codeAirlines);
            return ProcessResult(result);
        }

        [HttpGet("flight/{flightNumber}/{codeAirlines}/count")]
        public async Task<ActionResult<int>> GetInterestedCount(short flightNumber, string codeAirlines)
        {
            var result = await _subscriptionService.GetInterestedCountAsync(flightNumber, codeAirlines);
            return ProcessResult(result);
        }
    }
}
