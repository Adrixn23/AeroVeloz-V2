using AeroVeloz.Application.DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using AeroVeloz.Application.Contracts.Auth;


namespace AeroVelozDesktop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService) {

            _authenticationService = authenticationService;
        }

        // POST api/<AuthController>
        [HttpPost("login")]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public async Task<IActionResult> LoginAction([FromBody] UserLoginDto userLoginDto)
        {
            var result = await _authenticationService.LoginAsync(userLoginDto);
            if(result.Success) return Ok(result);
            return BadRequest(result);
        }

    }
}
