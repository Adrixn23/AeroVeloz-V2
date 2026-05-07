using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AeroVeloz.Application.DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using AeroVeloz.Application.Contracts.Auth;
using AeroVeloz.Application.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace AeroVeloz.Api.Controllers
{
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IAuthenticationService authenticationService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<object>> Login([FromBody] UserLoginDto userLoginDto)
        {
            var result = await _authenticationService.LoginAsync(userLoginDto);
            if (!result.Success || result.Value is null)
            {
                return ProcessResult(result);
            }

            var token = BuildToken(result.Value);

            return Ok(new
            {
                user = result.Value,
                accessToken = token.Token,
                tokenType = "Bearer",
                expiresAtUtc = token.ExpiresAtUtc
            });
        }

        private (string Token, DateTime ExpiresAtUtc) BuildToken(UserLoginResultDto user)
        {
            var issuer = _configuration["Jwt:Issuer"]!;
            var audience = _configuration["Jwt:Audience"]!;
            var key = _configuration["Jwt:Key"]!;
            var expires = DateTime.UtcNow.AddHours(2);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new("userId", user.UserId.ToString()),
                new("orgId", user.OrganizationId.ToString()),
                new(ClaimTypes.Name, user.UserName ?? string.Empty),
                new(ClaimTypes.Role, user.RoleName ?? string.Empty)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials);

            return (new JwtSecurityTokenHandler().WriteToken(jwtToken), expires);
        }
    }
}
