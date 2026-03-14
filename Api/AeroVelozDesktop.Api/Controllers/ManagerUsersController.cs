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
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ManagerUsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ManagerUsersController>
        [HttpPost("{}")]
        public async Task<IActionResult>  Post(UserSaveDto dto, Guid userId, int orgId)
        {
            var result = await _userService.CreateAsync(dto, userId, orgId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }


        // PUT api/<ManagerUsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ManagerUsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
