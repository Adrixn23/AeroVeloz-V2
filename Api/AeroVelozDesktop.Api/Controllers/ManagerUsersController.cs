using AeroVeloz.Application.Contracts.Users;
using AeroVeloz.Application.DTOs.Users;
using Microsoft.AspNetCore.Mvc;


namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ManagerUsersController : ControllerBase
    {

        public readonly IUserService _userService;
        public ManagerUsersController(IUserService userService) { 
                _userService = userService;
        }


        // GET: api/<ManagerUsersController>
        [HttpGet("organization/{orgId}")]
        public async Task<IActionResult>  GetAll([FromQuery] Guid userId, [FromRoute] int orgId)
        {
            var result = await _userService.GetUsersByOrganizationAsync(userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }


        // POST api/<ManagerUsersController>
        [HttpPost]
        public async Task<IActionResult>  Post([FromBody] UserSaveDto dto, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _userService.CreateAsync(dto, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }


        // PUT api/<ManagerUsersController>/5
        [HttpPut]
        public async Task<IActionResult>  Put([FromBody] UserUpdateDto dto, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _userService.UpdateAsync(dto, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{entityId}")]

        public async Task<IActionResult> Desactive([FromRoute] Guid entityId, [FromQuery] Guid userId, [FromQuery] int orgId)
        {
            var result = await _userService.DeactivateAsync(entityId, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

     
    }
}
