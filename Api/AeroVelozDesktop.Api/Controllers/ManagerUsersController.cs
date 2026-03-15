using AeroVeloz.Application.Contracts.Users;
using AeroVeloz.Application.DTOs.Users;
using Microsoft.AspNetCore.Mvc;


namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerUsersController : ControllerBase
    {

        public readonly IUserServicie _userService;
        public ManagerUsersController(IUserServicie userServicie) { 
                _userService = userServicie;
        }


        // GET: api/<ManagerUsersController>
        [HttpGet("GetUsersByOrganizationAsync/{orgId}")]
        public async Task<IActionResult>  GetAll(Guid userId, int orgId)
        {
            var result = await _userService.GetUsersByOrganizationAsync(userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

       
        // POST api/<ManagerUsersController>
        [HttpPost("{ManagerUsersController}")]
        public async Task<IActionResult>  Post(UserSaveDto dto, Guid userId, int orgId)
        {
            var result = await _userService.CreateAsync(dto, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }


        // PUT api/<ManagerUsersController>/5
        [HttpPut("UpdateAsync")]
        public async Task<IActionResult>  Put(UserUpdateDto dto, Guid userId, int orgId)
        {
            var result = await _userService.UpdateAsync(dto, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("Desactive")]

        public async Task<IActionResult> Desactive(Guid entityId, Guid userId, int orgId)
        {
            var result = await _userService.DeactivateAsync(entityId, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

     
    }
}
