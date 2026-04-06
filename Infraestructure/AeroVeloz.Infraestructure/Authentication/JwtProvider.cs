using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AeroVeloz.Application.Contracts.Auth;
using AeroVeloz.Application.DTOs.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AeroVeloz.Infraestructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _configuration;

        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserLoginResultDto user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new(JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty),
                new("OrgId", user.OrganizationId.ToString()),
                new("OrgName", user.OrganizationName ?? string.Empty),
                new("OrgType", user.OrganizationType ?? string.Empty),
                new(ClaimTypes.Role, user.RoleName ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var secretKey = _configuration["JwtOptions:SecretKey"] ?? "AeroVelozSuperSecretKey_12345678901234567890";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtOptions:Issuer"] ?? "AeroVelozApp",
                audience: _configuration["JwtOptions:Audience"] ?? "AeroVelozDesktop",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
