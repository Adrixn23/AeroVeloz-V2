using AeroVeloz.Application.Services.Result;
using AeroVeloz.Domain.Entities.Users.User;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AeroVeloz.Api.Controllers
{
    [Route("api/users")]
    [Authorize(Roles = "SYSTEMADMIN,AIRPORTADMIN")]
    public class UsersController : ApiBaseController
    {
        private readonly AeroVelozContext _context;

        public UsersController(AeroVelozContext context)
        {
            _context = context;
        }

        [HttpPost("staff")]
        public async Task<ActionResult<bool>> CreateStaff([FromBody] CreateUserDto dto)
        {
            try 
            {
                var hasher = new PasswordHasher<User>();
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    nameUser = dto.UserName,
                    passwordHash = hasher.HashPassword(null!, dto.Password),
                    isActive = true,
                    idOrganization = dto.OrganizationId,
                    idRol = dto.RoleId,
                    createAt = DateTime.UtcNow
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok(OperationResult<bool>.Ok(true, "Usuario creado con éxito"));
            }
            catch (Exception ex)
            {
                return BadRequest(OperationResult<bool>.Fail("USER_ERROR", $"Error al crear usuario: {ex.Message}"));
            }
        }

        [HttpGet("organization/{orgId:int}")]
        public async Task<ActionResult<IEnumerable<object>>> GetByOrg(int orgId)
        {
            var users = await _context.Users
                .Where(u => u.idOrganization == orgId)
                .Select(u => new { u.Id, u.nameUser, u.isActive, u.idRol })
                .ToListAsync();
            
            return Ok(OperationResult<IEnumerable<object>>.Ok(users));
        }
    }

    public class CreateUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
        public short RoleId { get; set; }
    }
}
