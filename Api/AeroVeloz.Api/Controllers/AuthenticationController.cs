using AeroVeloz.Application.DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using AeroVeloz.Application.Contracts.Auth;


namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public readonly IAuthenticationServicie _authenticationServicie;

        public AuthController(IAuthenticationServicie authenticationServicie)
        {

            _authenticationServicie = authenticationServicie;
        }

        // POST api/<AuthController>
        [HttpPost("{LoginAction}")]
        public async Task<IActionResult> LoginAction(UserLoginDto userLoginDto)
        {
            var result = await _authenticationServicie.LoginAsync(userLoginDto);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

    }
}