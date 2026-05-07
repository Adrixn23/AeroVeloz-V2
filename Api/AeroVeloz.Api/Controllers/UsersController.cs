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

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] CreateUserDto dto)
        {
            try 
            {
                var rowsAffected = await _context.Users
                    .Where(u => u.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(u => u.nameUser, dto.UserName)
                        .SetProperty(u => u.idRol, dto.RoleId));

                if (rowsAffected == 0) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(OperationResult<bool>.Fail("USER_UPDATE_ERROR", ex.Message));
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try 
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return NotFound();

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(OperationResult<bool>.Fail("USER_DELETE_ERROR", ex.Message));
            }
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
