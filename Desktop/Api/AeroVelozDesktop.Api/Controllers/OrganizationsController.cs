using AeroVeloz.Application.Contracts.Organization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationsController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetByType([FromRoute] string type)
        {
            var result = await _organizationService.GetOrganizationsByTypeAsync(type);
            if (result.Success)
                return Ok(result.Value);

            return BadRequest(result);
        }

        [HttpPut("{id}/block")]
        public async Task<IActionResult> BlockOrganization([FromRoute] int id)
        {
            var result = await _organizationService.BlockOrganizationAsync(id);
            if (result.Success)
                return Ok(result);
            
            return BadRequest(result);
        }
    }
}
